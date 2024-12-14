using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.Core.Services.IslamicFinance
{
    public interface IZakatService
    {
        Task<ZakatCalculation> CalculateZakat(decimal amount);
        Task<bool> ValidateZakatAmount(decimal amount);
        Task<decimal> GetNisabThreshold();
    }
}