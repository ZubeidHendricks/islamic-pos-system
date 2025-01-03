namespace IslamicPOS.Core.Models.Analytics;

public class PredictionRequest
{
    public string MetricType { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int ForecastPeriods { get; set; }
    public string ModelType { get; set; } = string.Empty;
}