namespace IslamicPOS.Core.Services.Partners;

public interface IPartnerService
{
    Task<Partner> AddPartnerAsync(Partner partner);
    Task<Partner> UpdatePartnerAsync(Partner partner);
    Task<bool> DeletePartnerAsync(int id);
    Task<Partner?> GetByIdAsync(int id);
    Task<List<Partner>> GetAllAsync();
    Task<List<Partner>> GetActivePartnersAsync();
}