using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Models.Analytics;

public class PredictionModel : Entity
{
    public DateTime Date { get; set; }
    public decimal ActualValue { get; set; }
    public decimal PredictedValue { get; set; }
    public string MetricType { get; set; } = string.Empty;
    public decimal Accuracy { get; set; }
    public string ModelType { get; set; } = string.Empty;
}