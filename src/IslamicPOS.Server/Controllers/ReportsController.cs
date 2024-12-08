using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IslamicPOS.Core.Services;

namespace IslamicPOS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportingService _reportingService;
    private readonly ILogger<ReportsController> _logger;

    public ReportsController(
        IReportingService reportingService,
        ILogger<ReportsController> logger)
    {
        _reportingService = reportingService;
        _logger = logger;
    }

    [HttpGet("sales")]
    public async Task<ActionResult<SalesSummary>> GetSalesSummary(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        try
        {
            var summary = await _reportingService.GetSalesSummaryAsync(startDate, endDate);
            return Ok(summary);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating sales summary");
            return StatusCode(500, "An error occurred while generating sales summary");
        }
    }

    [HttpGet("profit")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActionResult<ProfitReport>> GetProfitReport(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        try
        {
            var report = await _reportingService.GetProfitReportAsync(startDate, endDate);
            return Ok(report);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating profit report");
            return StatusCode(500, "An error occurred while generating profit report");
        }
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportReport(
        [FromQuery] ReportType type,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] string format = "pdf")
    {
        try
        {
            var report = await _reportingService.ExportReportAsync(type, startDate, endDate, format);
            var contentType = format.ToLower() == "pdf" ? 
                "application/pdf" : 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(report, contentType, $"{type}-report.{format}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error exporting report");
            return StatusCode(500, "An error occurred while exporting report");
        }
    }
}