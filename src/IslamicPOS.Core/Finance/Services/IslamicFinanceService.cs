using IslamicPOS.Core.Common;
using IslamicPOS.Core.Finance.Interfaces;

namespace IslamicPOS.Core.Finance.Services;

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

    public async Task<ZakatCalculation> CalculateZakat(Money amount)
    {
        try
        {
            return ZakatCalculation.Create(
                businessAssets: amount,
                cashAndEquivalents: Money.Create(0, amount.Currency));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating zakat");
            throw;
        }
    }

    public async Task<ProfitSharing> CalculateProfitSharing(Money totalAmount)
    {
        try
        {
            var merchantShare = _configuration
                .GetValue<decimal>("IslamicFinance:DefaultMerchantShare", 0.7m);

            return ProfitSharing.Create(totalAmount, merchantShare);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating profit sharing");
            throw;
        }
    }

    public async Task<bool> ValidateFinancialPeriod(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
            return false;

        var maxPeriodDays = _configuration
            .GetValue<int>("IslamicFinance:MaxFinancialPeriodDays", 365);

        return (endDate - startDate).TotalDays <= maxPeriodDays;
    }
}