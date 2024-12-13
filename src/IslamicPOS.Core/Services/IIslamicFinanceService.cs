using IslamicPOS.Core.Models;

namespace IslamicPOS.Core.Services
{
    public interface IIslamicFinanceService
    {
        Task<bool> ValidateTransaction(Transaction transaction);
        Task<bool> IsHalalProduct(Product product);
        bool IsValidPaymentMethod(PaymentMethod method);
        decimal CalculateZakat(decimal amount);
        (decimal merchantShare, decimal partnerShare) CalculateProfitSharing(decimal totalAmount);
        string GetTransactionComplianceNotice(Transaction transaction);
        Task<bool> ValidateFinancialPeriod(DateTime startDate, DateTime endDate);
    }
}