using IslamicPOS.Core.Models.Analytics;

namespace IslamicPOS.Core.Services;

public interface IPredictiveAnalyticsService
{
    Task<PredictionResult> GeneratePredictionsAsync(PredictionRequest request);
    Task<List<AnalyticsMetric>> GetHistoricalMetricsAsync(string metricType, DateTime startDate, DateTime endDate);
    Task<decimal> GetModelAccuracyAsync(string metricType, string modelType);
    Task SavePredictionAsync(PredictionModel prediction);
}