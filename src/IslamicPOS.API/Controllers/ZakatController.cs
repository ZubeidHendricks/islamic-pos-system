using Microsoft.AspNetCore.Mvc;
using IslamicPOS.Infrastructure.Services;
using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.API.Controllers;

public class ZakatController : BaseApiController
{
    private readonly IZakatService _zakatService;

    public ZakatController(IZakatService zakatService)
    {
        _zakatService = zakatService;
    }

    [HttpPost("calculate")]
    public async Task<ActionResult<ZakatCalculation>> CalculateZakat()
    {
        var calculation = await _zakatService.CalculateZakatAsync(DateTime.UtcNow);
        return Ok(calculation);
    }

    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<ZakatCalculation>>> GetHistory()
    {
        var history = await _zakatService.GetZakatHistoryAsync();
        return Ok(history);
    }

    [HttpGet("unpaid")]
    public async Task<ActionResult<decimal>> GetUnpaidZakat()
    {
        var unpaidAmount = await _zakatService.GetUnpaidZakatObligationAsync();
        return Ok(unpaidAmount);
    }

    [HttpPost("{calculationId}/pay")]
    public async Task<ActionResult> MarkAsPaid(Guid calculationId)
    {
        var success = await _zakatService.MarkZakatAsPaidAsync(calculationId);
        if (!success)
            return NotFound();
            
        return NoContent();
    }

    [HttpGet("threshold")]
    public async Task<ActionResult<decimal>> GetNisabThreshold()
    {
        var threshold = await _zakatService.GetNisabThresholdAsync(DateTime.UtcNow);
        return Ok(threshold);
    }
}