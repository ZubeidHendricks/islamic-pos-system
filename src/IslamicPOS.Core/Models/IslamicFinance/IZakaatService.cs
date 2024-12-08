namespace IslamicPOS.Core.Models.IslamicFinance;

public interface IZakaatService
{
    ZakaatCalculation Calculate(ZakaatInput input);
}