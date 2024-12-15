using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Finance.Interfaces;

public interface IIslamicFinanceService
{
    Task<ZakatCalculation> CalculateZakat(Money amount);
    Task<ProfitSharing> CalculateProfitSharing(Money totalAmount);
    Task<bool> ValidateFinancialPeriod(DateTime startDate, DateTime endDate);
}