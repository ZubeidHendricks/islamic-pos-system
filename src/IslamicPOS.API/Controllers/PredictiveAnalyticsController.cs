using IslamicPOS.Core.Models.Analytics;
using IslamicPOS.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PredictiveAnalyticsController : ControllerBase
{
    private readonly IPredictiveAnalyticsService _analyticsService;
    private readonly ILogger<PredictiveAnalyticsController> _logger;

    public PredictiveAnalyticsController(
        IPredictiveAnalyticsService analyticsService,
        ILogger<PredictiveAnalyticsController> logger)
    {
        _analyticsService = analyticsService;
        _logger = logger;
    }

    [HttpPost("predictions")]
    public async Task<ActionResult<PredictionResult>> GeneratePredictions(PredictionRequest request)
    {
        try
        {
            var result = await _analyticsService.GeneratePredictionsAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating predictions");
            return StatusCode(500, "An error occurred while generating predictions");
        }
    }

    [HttpGet("metrics/{metricType}")]
    public async Task<ActionResult<List<AnalyticsMetric>>> GetHistoricalMetrics(
        string metricType,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        try
        {
            var metrics = await _analyticsService.GetHistoricalMetricsAsync(metricType, startDate, endDate);
            return Ok(metrics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving historical metrics");
            return StatusCode(500, "An error occurred while retrieving historical metrics");
        }
    }

    [HttpGet("accuracy/{metricType}/{modelType}")]
    public async Task<ActionResult<decimal>> GetModelAccuracy(string metricType, string modelType)
    {
        try
        {
            var accuracy = await _analyticsService.GetModelAccuracyAsync(metricType, modelType);
            return Ok(accuracy);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving model accuracy");
            return StatusCode(500, "An error occurred while retrieving model accuracy");
        }
    }
}