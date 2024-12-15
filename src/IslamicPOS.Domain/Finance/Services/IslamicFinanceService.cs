using IslamicPOS.Core.Common;
using IslamicPOS.Core.Finance;
using IslamicPOS.Domain.Finance.Interfaces;

namespace IslamicPOS.Domain.Finance.Services;

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

    public async Task<ZakaatCalculation> CalculateZakaat(decimal amount, string currency)
    {
        try
        {
            var money = Money.Create(amount, currency);
            return ZakaatCalculation.Create(
                businessAssets: money,
                cashAndEquivalents: Money.Create(0, currency));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating zakat");
            throw;
        }
    }

    public async Task<MudarabahContract> CreateMudarabahContract(
        decimal investment,
        decimal profitShare,
        string investor,
        string project)
    {
        try
        {
            return MudarabahContract.Create(
                investmentAmount: Money.Create(investment),
                profitSharingRatio: profitShare,
                investorDetails: investor,
                projectDetails: project,
                startDate: DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Mudarabah contract");
            throw;
        }
    }

    public async Task<MusharakahContract> CreateMusharakahContract(
        decimal partner1Investment,
        decimal partner2Investment,
        decimal partner1Share,
        decimal partner2Share,
        string project)
    {
        try
        {
            return MusharakahContract.Create(
                partner1Investment: Money.Create(partner1Investment),
                partner2Investment: Money.Create(partner2Investment),
                partner1Share: partner1Share,
                partner2Share: partner2Share,
                projectDetails: project,
                startDate: DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating Musharakah contract");
            throw;
        }
    }
}