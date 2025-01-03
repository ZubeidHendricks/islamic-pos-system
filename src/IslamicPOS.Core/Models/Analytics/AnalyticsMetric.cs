namespace IslamicPOS.Core.Models.Analytics;

public class AnalyticsMetric
{
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public string MetricType { get; set; } = string.Empty;
}