using System.Net.Http;
using System.Text.Json;

namespace IslamicPOS.Core.Services.ZakaatCalculator;

public class ZakaatService : IZakaatService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private const decimal GOLD_NISAB_GRAMS = 85;
    private const decimal SILVER_NISAB_GRAMS = 595;

    public ZakaatService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<ZakaatCalculation> CalculateZakaat(ZakaatInput input)
    {
        var nisabThreshold = await GetCurrentNisabThreshold();
        var totalWealth = CalculateTotalWealth(input);
        
        var calculation = new ZakaatCalculation
        {
            CalculationDate = DateTime.UtcNow,
            TotalAssets = totalWealth,
            Liabilities = input.Liabilities,
            NisabThreshold = nisabThreshold,
            Details = new ZakaatDetails
            {
                Cash = input.Cash,
                BankAccounts = input.BankAccounts,
                Gold = input.Gold,
                Silver = input.Silver,
                Investments = input.Investments,
                BusinessInventory = input.BusinessInventory,
                AccountsReceivable = input.AccountsReceivable,
                OtherAssets = input.OtherAssets,
                TotalLiabilities = input.Liabilities
            }
        };

        calculation.ZakaatAmount = CalculateZakaatAmount(totalWealth, input.Liabilities, nisabThreshold);
        return calculation;
    }

    private decimal CalculateTotalWealth(ZakaatInput input)
    {
        return input.Cash +
               input.BankAccounts +
               input.Gold +
               input.Silver +
               input.Investments +
               input.BusinessInventory +
               input.AccountsReceivable +
               input.OtherAssets;
    }

    private decimal CalculateZakaatAmount(decimal totalWealth, decimal liabilities, decimal nisabThreshold)
    {
        var netWealth = totalWealth - liabilities;
        
        if (netWealth < nisabThreshold)
            return 0;

        return Math.Round(netWealth * 0.025m, 2); // 2.5% of net wealth
    }

    public async Task<decimal> GetCurrentNisabThreshold()
    {
        try
        {
            // Get current gold price
            var goldPrice = await GetGoldPrice();
            var goldNisab = GOLD_NISAB_GRAMS * goldPrice;

            // Get current silver price
            var silverPrice = await GetSilverPrice();
            var silverNisab = SILVER_NISAB_GRAMS * silverPrice;

            // Use the lower of the two as the Nisab threshold
            return Math.Min(goldNisab, silverNisab);
        }
        catch
        {
            // Fallback to a default value if API calls fail
            return 5000m; // Default conservative estimate
        }
    }

    private async Task<decimal> GetGoldPrice()
    {
        var apiKey = _configuration["MetalPriceAPI:Key"];
        var response = await _httpClient.GetAsync($"https://api.metalpriceapi.com/v1/latest?api_key={apiKey}&base=XAU&currencies=USD");
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<MetalPriceResponse>(content);
        return data?.Rates.USD ?? 0;
    }

    private async Task<decimal> GetSilverPrice()
    {
        var apiKey = _configuration["MetalPriceAPI:Key"];
        var response = await _httpClient.GetAsync($"https://api.metalpriceapi.com/v1/latest?api_key={apiKey}&base=XAG&currencies=USD");
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<MetalPriceResponse>(content);
        return data?.Rates.USD ?? 0;
    }
}