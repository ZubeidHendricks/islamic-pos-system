using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Finance.Models;

namespace IslamicPOS.Domain.Finance.Interfaces;

public interface IZakaatService
{
    Task<ZakaatCalculation> CalculateZakaat(ZakaatInput input);
    Task<ZakaatCalculation?> GetZakaatCalculation(Guid id);
    Task<List<ZakaatCalculation>> GetZakaatHistory(string userId);
}
