using IslamicPOS.Application.Services;
using IslamicPOS.Domain.Finance;

namespace IslamicPOS.Web.Services
{
    public interface IFinanceService
    {
        Task<ZakaatCalculation> CalculateZakaat(ZakaatInput input);
        Task<IslamicFinanceOptions> GetFinanceOptions();
    }
}