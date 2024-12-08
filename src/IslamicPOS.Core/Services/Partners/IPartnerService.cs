using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.Core.Services.Partners;

public interface IPartnerService
{
    Task<Partner> GetByIdAsync(Guid partnerId);
    Task<Partner> CreatePartnerAsync(Partner partner);
    Task<Partner> UpdatePartnerAsync(Partner partner);
    Task<List<Partner>> GetActivePartnersAsync();
    Task<List<Partner>> GetAllPartnersAsync();
}