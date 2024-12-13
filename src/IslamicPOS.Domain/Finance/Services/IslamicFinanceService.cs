using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.ValueObjects;

namespace IslamicPOS.Domain.Finance.Services
{
    public class IslamicFinanceService : IIslamicFinanceService
    {
        private readonly IslamicFinanceOptions _options;

        public IslamicFinanceService(IslamicFinanceOptions options)
        {
            _options = options;
        }

        public bool ValidateTransaction(Transaction transaction)
        {
            // Check if transaction type is Halal
            if (!IsHalalTransaction(transaction))
                return false;

            // Validate payment method
            if (!IsValidPaymentMethod(transaction.PaymentMethod))
                return false;

            return true;
        }

        public bool IsHalalTransaction(Transaction transaction)
        {
            // Check for prohibited items
            foreach (var item in transaction.Items)
            {
                if (item.Product.IsHaram)
                    return false;
            }

            // Check for riba (interest)
            if (transaction.InterestRate > 0)
                return false;

            return true;
        }

        public bool IsValidPaymentMethod(PaymentMethod method)
        {
            // Validate according to Islamic principles
            switch (method)
            {
                case PaymentMethod.Cash:
                case PaymentMethod.BankTransfer:
                case PaymentMethod.DigitalWallet:
                    return true;
                case PaymentMethod.IslamicCredit: // Special Islamic credit card
                    return true;
                case PaymentMethod.ConventionalCredit: // Regular credit cards might involve riba
                    return false;
                default:
                    return false;
            }
        }

        public decimal CalculateZakat(Money amount)
        {
            if (amount < _options.NisabThreshold)
                return 0;

            return amount.Amount * _options.ZakaatRate;
        }

        public (decimal merchantShare, decimal partnerShare) CalculateProfitSharing(decimal totalProfit)
        {
            decimal merchantShare = totalProfit * _options.DefaultProfitSharingRatio;
            decimal partnerShare = totalProfit - merchantShare;
            return (merchantShare, partnerShare);
        }

        public bool ValidateFinancingTerm(int months)
        {
            return months <= _options.FinancingTermInMonths;
        }

        public string GetComplianceNotice(Transaction transaction)
        {
            if (!ValidateTransaction(transaction))
                return "This transaction may not comply with Islamic finance principles.";

            var notice = new StringBuilder();
            notice.AppendLine("This transaction complies with Islamic finance principles.");
            
            if (transaction.Amount >= _options.NisabThreshold)
            {
                decimal zakat = CalculateZakat(transaction.Amount);
                notice.AppendLine($"Zakat applicable: {zakat:C}");
            }

            return notice.ToString();
        }
    }

    public interface IIslamicFinanceService
    {
        bool ValidateTransaction(Transaction transaction);
        bool IsHalalTransaction(Transaction transaction);
        bool IsValidPaymentMethod(PaymentMethod method);
        decimal CalculateZakat(Money amount);
        (decimal merchantShare, decimal partnerShare) CalculateProfitSharing(decimal totalProfit);
        bool ValidateFinancingTerm(int months);
        string GetComplianceNotice(Transaction transaction);
    }
}