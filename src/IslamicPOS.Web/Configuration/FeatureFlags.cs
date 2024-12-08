namespace IslamicPOS.Web.Configuration;

public class FeatureFlags
{
    public const string NewUI = "new-ui";
    public const string DetailedZakaah = "zakaah-calculator";
    public const string AdvancedReporting = "advanced-reporting";
    public const string BetaFeatures = "beta-features";
}

public class FeatureConfiguration
{
    public Dictionary<string, EnvironmentFeatureFlags> Environments { get; set; }
}

public class EnvironmentFeatureFlags
{
    public bool NewUI { get; set; }
    public bool DetailedZakaah { get; set; }
    public bool AdvancedReporting { get; set; }
    public bool BetaFeatures { get; set; }
}