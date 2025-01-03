using IslamicPOS.Core.Models.Analytics;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;

namespace IslamicPOS.Infrastructure.Services;

public class PredictiveAnalyticsService : IPredictiveAnalyticsService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<PredictiveAnalyticsService> _logger;

    public PredictiveAnalyticsService(
        ApplicationDbContext context,
        ILogger<PredictiveAnalyticsService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PredictionResult> GeneratePredictionsAsync(PredictionRequest request)
    {
        try
        {
            var historicalData = await GetHistoricalMetricsAsync(
                request.MetricType,
                request.StartDate,
                request.EndDate
            );

            if (!historicalData.Any())
            {
                throw new InvalidOperationException("Insufficient historical data for predictions");
            }

            var result = new PredictionResult
            {
                ModelType = request.ModelType,
                Predictions = new List<PredictionPoint>()
            };

            result.Predictions.AddRange(historicalData.Select(h => new PredictionPoint
            {
                Date = h.Date,
                Value = h.Value,
                IsHistorical = true
            }));

            var predictions = request.ModelType switch
            {
                "EMA" => GenerateEMAPredictions(historicalData, request.ForecastPeriods),
                "LinearRegression" => GenerateLinearRegressionPredictions(historicalData, request.ForecastPeriods),
                _ => throw new ArgumentException("Invalid model type")
            };

            result.Predictions.AddRange(predictions.Select(p => new PredictionPoint
            {
                Date = p.Date,
                Value = p.Value,
                IsHistorical = false
            }));

            result.ModelAccuracy = await CalculateModelAccuracyAsync(
                request.MetricType,
                request.ModelType,
                historicalData
            );

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating predictions");
            throw;
        }
    }

    // Rest of the implementation...
}