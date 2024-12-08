using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.Core.Services.IslamicFinance;

public interface IZakaatService
{
    ZakaatCalculation Calculate(ZakaatInput input);
}