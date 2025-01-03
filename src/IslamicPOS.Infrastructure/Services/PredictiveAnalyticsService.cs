using IslamicPOS.Core.Models.Analytics;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MathNet.Numerics.Statistics;

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

    public async Task<List<AnalyticsMetric>> GetHistoricalMetricsAsync(
        string metricType,
        DateTime startDate,
        DateTime endDate)
    {
        var metrics = new List<AnalyticsMetric>();

        switch (metricType)
        {
            case "ProfitShare":
                var profitShares = await _context.ProfitDistributionDetails
                    .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                    .GroupBy(p => p.PaymentDate!.Value.Date)
                    .Select(g => new AnalyticsMetric
                    {
                        Date = g.Key,
                        Value = g.Sum(p => p.Amount),
                        MetricType = metricType
                    })
                    .ToListAsync();
                metrics.AddRange(profitShares);
                break;

            case "Revenue":
                // Add revenue metrics when available
                break;
        }

        return metrics.OrderBy(m => m.Date).ToList();
    }

    public async Task<decimal> GetModelAccuracyAsync(string metricType, string modelType)
    {
        var recentPredictions = await _context.Set<PredictionModel>()
            .Where(p => p.MetricType == metricType && p.ModelType == modelType)
            .OrderByDescending(p => p.Date)
            .Take(30)
            .ToListAsync();

        if (!recentPredictions.Any())
            return 0;

        return recentPredictions.Average(p => p.Accuracy);
    }

    public async Task SavePredictionAsync(PredictionModel prediction)
    {
        _context.Add(prediction);
        await _context.SaveChangesAsync();
    }

    private List<PredictionPoint> GenerateEMAPredictions(
        List<AnalyticsMetric> historicalData,
        int forecastPeriods)
    {
        var predictions = new List<PredictionPoint>();
        var values = historicalData.Select(h => (double)h.Value).ToList();
        
        double multiplier = 2.0 / (values.Count + 1);
        double ema = values[0];
        
        for (int i = 1; i < values.Count; i++)
        {
            ema = (values[i] * multiplier) + (ema * (1 - multiplier));
        }

        var lastDate = historicalData.Last().Date;
        double lastValue = (double)historicalData.Last().Value;
        double trend = (ema - lastValue) / lastValue;

        for (int i = 1; i <= forecastPeriods; i++)
        {
            var predictedValue = lastValue * Math.Pow(1 + trend, i);
            predictions.Add(new PredictionPoint
            {
                Date = lastDate.AddDays(i),
                Value = (decimal)predictedValue,
                IsHistorical = false
            });
        }

        return predictions;
    }

    private List<PredictionPoint> GenerateLinearRegressionPredictions(
        List<AnalyticsMetric> historicalData,
        int forecastPeriods)
    {
        var predictions = new List<PredictionPoint>();
        var xValues = Enumerable.Range(0, historicalData.Count).Select(i => (double)i).ToList();
        var yValues = historicalData.Select(h => (double)h.Value).ToList();

        var regression = new SimpleRegression();
        for (int i = 0; i < xValues.Count; i++)
        {
            regression.Add(xValues[i], yValues[i]);
        }

        var lastDate = historicalData.Last().Date;
        for (int i = 1; i <= forecastPeriods; i++)
        {
            var x = xValues.Count + i;
            var predictedValue = regression.Predict(x);
            
            predictions.Add(new PredictionPoint
            {
                Date = lastDate.AddDays(i),
                Value = (decimal)predictedValue,
                IsHistorical = false
            });
        }

        return predictions;
    }

    private async Task<decimal> CalculateModelAccuracyAsync(
        string metricType,
        string modelType,
        List<AnalyticsMetric> actualData)
    {
        var predictions = await _context.Set<PredictionModel>()
            .Where(p => p.MetricType == metricType && 
                       p.ModelType == modelType &&
                       actualData.Any(a => a.Date == p.Date))
            .ToListAsync();

        if (!predictions.Any())
            return 0;

        var errors = predictions.Select(p =>
        {
            var actual = actualData.First(a => a.Date == p.Date).Value;
            return Math.Abs((actual - p.PredictedValue) / actual);
        });

        return 1 - (decimal)errors.Average();
    }
}