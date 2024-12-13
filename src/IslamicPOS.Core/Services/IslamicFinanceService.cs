using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;
using IslamicPOS.Core.Models;

namespace IslamicPOS.Core.Services
{
    public class IslamicFinanceService : IIslamicFinanceService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IslamicFinanceService> _logger;

        public IslamicFinanceService(IConfiguration configuration, ILogger<IslamicFinanceService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> ValidateTransaction(Transaction transaction)
        {
            try
            {
                foreach (var item in transaction.Items)
                {
                    if (!await IsHalalProduct(item.Product))
                    {
                        _logger.LogWarning($"Non-halal product found: {item.ProductId}");
                        return false;
                    }
                }

                if (!IsValidPaymentMethod(transaction.PaymentMethod))
                {
                    _logger.LogWarning($"Invalid payment method: {transaction.PaymentMethod}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating transaction");
                return false;
            }
        }

        public async Task<bool> IsHalalProduct(Product product)
        {
            var nonHalalCategories = _configuration.GetSection("IslamicFinance:NonHalalCategories").Get<string[]>();
            if (nonHalalCategories?.Contains(product.Category) == true)
                return false;

            return true;
        }

        public bool IsValidPaymentMethod(PaymentMethod method)
        {
            return method switch
            {
                PaymentMethod.Cash => true,
                PaymentMethod.BankTransfer => true,
                PaymentMethod.IslamicDebit => true,
                PaymentMethod.IslamicCredit => true,
                PaymentMethod.DigitalWallet => true,
                _ => false
            };
        }

        public decimal CalculateZakat(decimal amount)
        {
            var nisabThreshold = _configuration.GetValue<decimal>("IslamicFinance:NisabThreshold");
            var zakatRate = _configuration.GetValue<decimal>("IslamicFinance:ZakatRate");

            if (amount < nisabThreshold)
                return 0;

            return Math.Round(amount * zakatRate, 2);
        }

        public (decimal merchantShare, decimal partnerShare) CalculateProfitSharing(decimal totalAmount)
        {
            var profitMargin = _configuration.GetValue<decimal>("IslamicFinance:ProfitMargin");
            var merchantRatio = _configuration.GetValue<decimal>("IslamicFinance:MerchantProfitRatio");

            var totalProfit = totalAmount * profitMargin;
            var merchantShare = Math.Round(totalProfit * merchantRatio, 2);
            var partnerShare = Math.Round(totalProfit - merchantShare, 2);

            return (merchantShare, partnerShare);
        }

        public string GetTransactionComplianceNotice(Transaction transaction)
        {
            var notice = new StringBuilder();
            notice.AppendLine("Islamic Finance Compliance Notice");
            notice.AppendLine("--------------------------------");

            var zakatAmount = CalculateZakat(transaction.TotalAmount);
            if (zakatAmount > 0)
            {
                notice.AppendLine($"Zakat Applicable: {zakatAmount:C}");
            }

            var (merchantShare, partnerShare) = CalculateProfitSharing(transaction.TotalAmount);
            notice.AppendLine($"Profit Distribution:");
            notice.AppendLine($"- Merchant Share: {merchantShare:C}");
            notice.AppendLine($"- Partner Share: {partnerShare:C}");

            return notice.ToString();
        }

        public Task<bool> ValidateFinancialPeriod(DateTime startDate, DateTime endDate)
        {
            return Task.FromResult(true);
        }
    }
}