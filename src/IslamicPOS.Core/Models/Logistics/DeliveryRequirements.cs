namespace IslamicPOS.Core.Models.Logistics;

public class DeliveryRequirements
{
    public decimal TotalWeightKg { get; set; }
    public decimal TotalVolumeM3 { get; set; }
    public bool RequiresRefrigeration { get; set; }
    public decimal? RequiredTemperature { get; set; }
    public bool RequiresHalalCertification { get; set; }
    public List<string> SpecialRequirements { get; set; } = new();
    public TimeWindow DeliveryWindow { get; set; } = new();
    public List<string> DeliveryZones { get; set; } = new();
}

public class TimeWindow
{
    public DateTime EarliestTime { get; set; }
    public DateTime LatestTime { get; set; }
    public List<string> ExcludedTimes { get; set; } = new();
}

public class DeliveryProof
{
    public string SignatureImage { get; set; } = string.Empty;
    public string ProofOfDeliveryImage { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string RecipientName { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public List<string> Issues { get; set; } = new();
}

public class DeliveryIssue
{
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string ReportedBy { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Resolution { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new();
}

public class DeliveryMetrics
{
    public int TotalDeliveries { get; set; }
    public int CompletedDeliveries { get; set; }
    public int FailedDeliveries { get; set; }
    public decimal OnTimeDeliveryRate { get; set; }
    public decimal AverageDeliveryTime { get; set; }
    public decimal TotalDistance { get; set; }
    public decimal FuelConsumption { get; set; }
    public int TotalStops { get; set; }
    public decimal AverageStopsPerRoute { get; set; }
    public List<string> CommonIssues { get; set; } = new();
}