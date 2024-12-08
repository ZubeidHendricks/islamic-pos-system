namespace IslamicPOS.Web.Services.ABTesting;

public class ABTestingService : IABTestingService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ABTestingService> _logger;
    private readonly Dictionary<string, Dictionary<string, double>> _featureVariants;

    public ABTestingService(
        IConfiguration configuration,
        ILogger<ABTestingService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _featureVariants = new Dictionary<string, Dictionary<string, double>>
        {
            {
                "new-ui", new Dictionary<string, double>
                {
                    { "control", 0.5 },
                    { "variant", 0.5 }
                }
            },
            {
                "zakaah-calculator", new Dictionary<string, double>
                {
                    { "simple", 0.3 },
                    { "detailed", 0.7 }
                }
            }
        };
    }

    public string GetVariant(string feature, string userId)
    {
        try
        {
            if (!_featureVariants.ContainsKey(feature))
                return "control";

            var variants = _featureVariants[feature];
            var hash = Math.Abs(userId.GetHashCode());
            var normalized = (double)(hash % 100) / 100;

            double cumulative = 0;
            foreach (var variant in variants)
            {
                cumulative += variant.Value;
                if (normalized <= cumulative)
                    return variant.Key;
            }

            return variants.First().Key;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error determining variant for feature {Feature}", feature);
            return "control";
        }
    }

    public async Task TrackEvent(string feature, string variant, string eventName, string userId)
    {
        try
        {
            // Track the event in Application Insights
            var telemetry = new EventTelemetry(eventName);
            telemetry.Properties["Feature"] = feature;
            telemetry.Properties["Variant"] = variant;
            telemetry.Properties["UserId"] = userId;

            TelemetryClient.TrackEvent(telemetry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error tracking A/B test event");
        }
    }
}