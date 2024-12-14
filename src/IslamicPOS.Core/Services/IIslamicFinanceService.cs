using IslamicPOS.Core.Models.Transaction;
using IslamicPOS.Core.Models.Product;
using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.Core.Services
{
    public interface IIslamicFinanceService
    {
        Task<ZakatCalculation> CalculateZakat(Transaction transaction);
        Task<bool> ValidateTransaction(Transaction transaction);
        Task<bool> IsHalalProduct(Product product);
        bool IsValidPaymentMethod(PaymentMethod method);
        Task<ProfitSharing> CalculateProfitSharing(Transaction transaction);
    }
}