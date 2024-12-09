using System.Net.Http.Json;
using IslamicPOS.Application.Services;
using IslamicPOS.Domain.Finance;

namespace IslamicPOS.Web.Services
{
    public class FinanceService : IFinanceService
    {
        private readonly HttpClient _httpClient;

        public FinanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ZakaatCalculation> CalculateZakaat(ZakaatInput input)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/finance/zakaat", input);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ZakaatCalculation>() 
                    ?? throw new Exception("Failed to deserialize response");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to calculate Zakaat: {ex.Message}", ex);
            }
        }

        public async Task<IslamicFinanceOptions> GetFinanceOptions()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IslamicFinanceOptions>("api/finance/options")
                    ?? throw new Exception("Failed to retrieve finance options");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get finance options: {ex.Message}", ex);
            }
        }
    }
}