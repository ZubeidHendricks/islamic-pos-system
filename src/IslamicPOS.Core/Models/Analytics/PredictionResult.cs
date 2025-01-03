namespace IslamicPOS.Core.Models.Analytics;

public class PredictionResult
{
    public List<PredictionPoint> Predictions { get; set; } = new();
    public decimal ModelAccuracy { get; set; }
    public string ModelType { get; set; } = string.Empty;
}

public class PredictionPoint
{
    public DateTime Date { get; set; }
    public decimal Value { get; set; }
    public bool IsHistorical { get; set; }
}