using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IslamicPOS.Core.Models.Transaction;
using IslamicPOS.Core.Models.Product;
using IslamicPOS.Core.Models.Common;
using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.Core.Services
{
    public class IslamicFinanceService : IIslamicFinanceService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IslamicFinanceService> _logger;

        public IslamicFinanceService(
            IConfiguration configuration,
            ILogger<IslamicFinanceService> logger)
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
                        _logger.LogWarning($"Non-halal product in transaction: {item.ProductId}");
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

        public async Task<bool> IsHalalProduct(ProductItem product)
        {
            // Check product category compliance
            var nonHalalCategories = _configuration
                .GetSection("IslamicFinance:NonHalalCategories")
                .Get<string[]>();

            if (nonHalalCategories?.Contains(product.Category.Name) == true)
                return false;

            return product.IsHalalVerified;
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

        public async Task<ZakatCalculation> CalculateZakat(Money amount)
        {
            var nisabThreshold = Money.Create(
                _configuration.GetValue<decimal>("IslamicFinance:NisabThreshold"));
            
            return new ZakatCalculation(amount, nisabThreshold);
        }

        public async Task<ProfitSharing> CalculateProfitSharing(Money totalAmount)
        {
            var merchantShare = _configuration
                .GetValue<decimal>("IslamicFinance:DefaultMerchantShare", 0.7m);

            return new ProfitSharing(totalAmount, merchantShare);
        }

        public async Task<string> GetTransactionComplianceNotice(Transaction transaction)
        {
            var notice = await ValidateTransaction(transaction) 
                ? "This transaction complies with Islamic finance principles."
                : "This transaction may not comply with Islamic finance principles.";

            return notice;
        }

        public async Task<bool> ValidateFinancialPeriod(DateTime startDate, DateTime endDate)
        {
            // Implement financial period validation logic
            return true;
        }
    }
}