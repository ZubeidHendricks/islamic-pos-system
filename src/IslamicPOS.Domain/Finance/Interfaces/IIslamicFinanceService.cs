using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Finance.Models;
using IslamicPOS.Domain.Sales;

namespace IslamicPOS.Domain.Finance.Interfaces;

public interface IIslamicFinanceService
{
    bool ValidateTransaction(Transaction transaction);
    ZakaatCalculation CalculateZakaat(Money assets);
    MudarabahContract CreateMudarabahContract(Money investment, decimal profitRatio);
    MusharakahContract CreateMusharakahContract(Money capital, decimal businessShare, decimal investorShare);
}
