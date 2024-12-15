using IslamicPOS.Core.Finance;

namespace IslamicPOS.Domain.Finance.Interfaces;

public interface IIslamicFinanceService
{
    Task<ZakaatCalculation> CalculateZakaat(decimal amount, string currency);
    Task<MudarabahContract> CreateMudarabahContract(decimal investment, decimal profitShare, string investor, string project);
    Task<MusharakahContract> CreateMusharakahContract(decimal partner1Investment, decimal partner2Investment, decimal partner1Share, decimal partner2Share, string project);
}