using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalesController : ControllerBase
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly ILogger<SalesController> _logger;

    public SalesController(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        ILogger<SalesController> logger)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<Sale>> CreateSale([FromBody] SaleRequest request)
    {
        try
        {
            var sale = new Sale
            {
                Date = DateTime.UtcNow,
                Items = request.Items.Select(i => new SaleItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Total = i.Quantity * i.UnitPrice
                }).ToList(),
                SubTotal = request.Items.Sum(i => i.Quantity * i.UnitPrice),
                TaxAmount = request.Items.Sum(i => i.Quantity * i.UnitPrice) * 0.05m,
                PaymentMethod = request.PaymentMethod,
                CustomerName = request.CustomerName
            };
            sale.Total = sale.SubTotal + sale.TaxAmount;

            // Update inventory
            foreach (var item in request.Items)
            {
                await _productRepository.UpdateStockAsync(item.ProductId, -item.Quantity);
            }

            var result = await _saleRepository.AddAsync(sale);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating sale");
            return StatusCode(500, "An error occurred while processing the sale");
        }
    }

    [HttpGet("daily-summary")]
    public async Task<ActionResult<SaleSummary>> GetDailySummary([FromQuery] DateTime date)
    {
        try
        {
            var summary = await _saleRepository.GetDailySummaryAsync(date);
            return Ok(summary);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting daily summary");
            return StatusCode(500, "An error occurred while getting daily summary");
        }
    }

    [HttpGet("top-products")]
    public async Task<ActionResult<IEnumerable<SalesByProduct>>> GetTopProducts(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] int count = 10)
    {
        try
        {
            var products = await _saleRepository.GetTopProductsAsync(startDate, endDate, count);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting top products");
            return StatusCode(500, "An error occurred while getting top products");
        }
    }
}