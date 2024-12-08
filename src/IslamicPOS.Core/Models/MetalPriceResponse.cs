namespace IslamicPOS.Core.Models;

public class MetalPriceResponse
{
    public bool Success { get; set; }
    public string Base { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public MetalRates Rates { get; set; } = new();
}

public class MetalRates
{
    public decimal USD { get; set; }
}