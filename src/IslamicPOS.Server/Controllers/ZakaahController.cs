using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IslamicPOS.Core.Services;
using IslamicPOS.Core.Models;

namespace IslamicPOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ZakaahController : ControllerBase
{
    private readonly IZakaahCalculator _calculator;
    private readonly IProductRepository _productRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<ZakaahController> _logger;

    public ZakaahController(
        IZakaahCalculator calculator,
        IProductRepository productRepository,
        ISaleRepository saleRepository,
        ILogger<ZakaahController> logger)
    {
        _calculator = calculator;
        _productRepository = productRepository;
        _saleRepository = saleRepository;
        _logger = logger;
    }

    [HttpPost("calculate")]
    public async Task<ActionResult<ZakaahResult>> CalculateZakaah([FromBody] ZakaahRequest request)
    {
        try
        {
            var totalAssets = request.CashOnHand + request.InventoryValue + request.OtherAssets;
            var isEligible = await _calculator.IsEligibleForZakaah(totalAssets, request.Currency);
            var zakaahAmount = isEligible ? totalAssets * 0.025m : 0;

            return Ok(new ZakaahResult
            {
                TotalAssets = totalAssets,
                IsEligible = isEligible,
                ZakaahAmount = zakaahAmount,
                Currency = request.Currency,
                CalculationDate = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating Zakaah");
            return StatusCode(500, "An error occurred while calculating Zakaah");
        }
    }

    [HttpGet("thresholds")]
    public ActionResult<Dictionary<string, decimal>> GetNisabThresholds()
    {
        return Ok(new Dictionary<string, decimal>
        {
            { "USD", 5200m },
            { "EUR", 4800m },
            { "GBP", 4100m }
        });
    }
}
