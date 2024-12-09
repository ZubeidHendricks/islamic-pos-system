using IslamicPOS.Domain.ValueObjects;

namespace IslamicPOS.Domain.Finance.Interfaces
{
    public interface IIslamicFinanceService
    {
        Task<ZakaatCalculation> CalculateZakaat(ZakaatParameters parameters);
        Task<MudarabahContract> CreateMudarabahContract(MudarabahParameters parameters);
        Task<MusharakahContract> CreateMusharakahContract(MusharakahParameters parameters);
        Task<IslamicFinanceOptions> GetFinanceOptions();
    }

    public record ZakaatParameters(
        Money TotalAssets,
        Money Liabilities,
        Money BusinessAssets,
        Money Investments
    );

    public record MudarabahParameters(
        string InvestorId,
        string ManagerId,
        Money InvestmentAmount,
        decimal ProfitSharingRatio,
        int TermInMonths
    );

    public record MusharakahParameters(
        List<PartnerContribution> PartnerContributions,
        Money TotalCapital,
        int TermInMonths
    );

    public record PartnerContribution(
        string PartnerId,
        Money ContributionAmount,
        decimal OwnershipPercentage
    );
}