using IslamicPOS.Infrastructure.Repositories;

namespace IslamicPOS.Infrastructure.Services;

public class ZakatService : IZakatService
{
    private readonly IZakatCalculationRepository _zakatRepository;
    private readonly IProductRepository _productRepository;
    private readonly IFinancialService _financialService;
    private const decimal ZAKAT_RATE = 0.025m; // 2.5%

    public ZakatService(
        IZakatCalculationRepository zakatRepository,
        IProductRepository productRepository,
        IFinancialService financialService)
    {
        _zakatRepository = zakatRepository;
        _productRepository = productRepository;
        _financialService = financialService;
    }

    public async Task<ZakatCalculation> CalculateZakatAsync(DateTime calculationDate)
    {
        var totalAssets = await GetTotalAssetsValueAsync();
        var totalLiabilities = await GetTotalLiabilitiesAsync();
        var netWorth = totalAssets - totalLiabilities;
        var nisabThreshold = await GetNisabThresholdAsync(calculationDate);
        var isEligible = netWorth >= nisabThreshold;
        
        var calculation = new ZakatCalculation
        {
            CalculationDate = calculationDate,
            TotalAssets = totalAssets,
            TotalLiabilities = totalLiabilities,
            NetWorth = netWorth,
            NisabThreshold = nisabThreshold,
            IsEligible = isEligible,
            ZakatAmount = isEligible ? netWorth * ZAKAT_RATE : 0,
            CalculationMethod = "Standard Business Zakat",
            IsPaid = false
        };

        return await _zakatRepository.AddAsync(calculation);
    }

    public async Task<bool> IsEligibleForZakatAsync(decimal netWorth, DateTime calculationDate)
    {
        var nisabThreshold = await GetNisabThresholdAsync(calculationDate);
        return netWorth >= nisabThreshold;
    }

    public async Task<decimal> GetNisabThresholdAsync(DateTime date)
    {
        // This should ideally fetch current gold/silver prices from an API
        // For now, using a fixed value based on gold price
        const decimal GOLD_PRICE_PER_GRAM = 60m; // Example price
        const decimal NISAB_GOLD_WEIGHT = 85m; // 85 grams of gold
        
        return GOLD_PRICE_PER_GRAM * NISAB_GOLD_WEIGHT;
    }

    public async Task<decimal> GetTotalAssetsValueAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var inventoryValue = products.Sum(p => p.Cost * p.StockQuantity);
        
        // Add other assets (cash, receivables, etc.)
        // This is a simplified calculation
        return inventoryValue;
    }

    public async Task<decimal> GetTotalLiabilitiesAsync()
    {
        // This should include all business liabilities
        // For now, returning a placeholder value
        return 0;
    }

    public async Task<IEnumerable<ZakatCalculation>> GetZakatHistoryAsync()
    {
        return await _zakatRepository.GetAllAsync();
    }

    public async Task<decimal> GetUnpaidZakatObligationAsync()
    {
        return await _zakatRepository.GetTotalUnpaidZakatAsync();
    }

    public async Task<bool> MarkZakatAsPaidAsync(Guid calculationId)
    {
        var calculation = await _zakatRepository.GetByIdAsync(calculationId);
        if (calculation == null || calculation.IsPaid) return false;

        calculation.IsPaid = true;
        calculation.PaymentDate = DateTime.UtcNow;
        await _zakatRepository.UpdateAsync(calculation);
        return true;
    }
}