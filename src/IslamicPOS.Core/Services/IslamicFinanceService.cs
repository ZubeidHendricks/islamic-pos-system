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
                // Check product compliance
                foreach (var item in transaction.Items)
                {
                    if (!await IsHalalProduct(item.Product))
                    {
                        _logger.LogWarning($"Non-halal product found in transaction: {item.ProductId}");
                        return false;
                    }
                }

                // Validate payment method
                if (!IsValidPaymentMethod(transaction.PaymentMethod))
                {
                    _logger.LogWarning($"Invalid payment method: {transaction.PaymentMethod}");
                    return false;
                }

                // Validate financial period
                if (!await ValidateFinancialPeriod(transaction.Timestamp.Date, transaction.Timestamp.Date))
                {
                    _logger.LogWarning("Transaction date falls in invalid financial period");
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
            // Check product category compliance
            var nonHalalCategories = _configuration.GetSection("NonHalalCategories").Get<string[]>();
            if (nonHalalCategories?.Contains(product.Category) == true)
                return false;

            // Check product ingredients/attributes
            if (product.Attributes?.Any(a => a.Key == "IsHalal" && a.Value.ToString() == "false") == true)
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

            // Add Zakat information if applicable
            var zakatAmount = CalculateZakat(transaction.TotalAmount);
            if (zakatAmount > 0)
            {
                notice.AppendLine($"Zakat Applicable: {zakatAmount:C}");
            }

            // Add profit sharing information
            var (merchantShare, partnerShare) = CalculateProfitSharing(transaction.TotalAmount);
            notice.AppendLine($"Profit Distribution:");
            notice.AppendLine($"- Merchant Share: {merchantShare:C}");
            notice.AppendLine($"- Partner Share: {partnerShare:C}");

            return notice.ToString();
        }

        public async Task<bool> ValidateFinancialPeriod(DateTime startDate, DateTime endDate)
        {
            // Implement financial period validation logic
            // This could include checking against Islamic calendar
            // or specific financial rules
            return true; // Placeholder implementation
        }
    }
}