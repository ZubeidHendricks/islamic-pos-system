using IslamicPOS.Core.Models.Transaction;
using IslamicPOS.Core.Models.Product;
using IslamicPOS.Core.Models.Common;
using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.Core.Services
{
    public interface IIslamicFinanceService
    {
        Task<bool> ValidateTransaction(Transaction transaction);
        Task<bool> IsHalalProduct(ProductItem product);
        bool IsValidPaymentMethod(PaymentMethod method);
        Task<ZakatCalculation> CalculateZakat(Money amount);
        Task<ProfitSharing> CalculateProfitSharing(Money totalAmount);
        Task<string> GetTransactionComplianceNotice(Transaction transaction);
        Task<bool> ValidateFinancialPeriod(DateTime startDate, DateTime endDate);
    }
}