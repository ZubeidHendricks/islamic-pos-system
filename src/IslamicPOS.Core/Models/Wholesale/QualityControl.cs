namespace IslamicPOS.Core.Models.Wholesale;

public class QualityControl
{
    public Guid Id { get; set; }
    public Guid InspectionId { get; set; }
    public DateTime InspectionDate { get; set; }
    public string InspectorId { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public int BatchNumber { get; set; }
    public string BatchCode { get; set; } = string.Empty;
    public int SampleSize { get; set; }
    public int DefectCount { get; set; }
    public string DefectType { get; set; } = string.Empty;
    public InspectionResult Result { get; set; }
    public string Notes { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new();
    public bool RequiresManagerReview { get; set; }
    public string ManagerReviewNotes { get; set; } = string.Empty;
    public DateTime? ReviewDate { get; set; }
    public bool IsApproved { get; set; }
    public List<QualityCheckpoint> Checkpoints { get; set; } = new();
    public List<HalalCompliance> HalalChecks { get; set; } = new();
    
    public decimal DefectRate => SampleSize > 0 ? (decimal)DefectCount / SampleSize * 100 : 0;
}

public class QualityCheckpoint
{
    public string Name { get; set; } = string.Empty;
    public bool Required { get; set; }
    public CheckpointPriority Priority { get; set; }
    public bool IsPassed { get; set; }
    public string Notes { get; set; } = string.Empty;
    public int? SampleSize { get; set; }
}

public class HalalCompliance
{
    public string CertificateNumber { get; set; } = string.Empty;
    public string IssuingBody { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsVerified { get; set; }
    public List<string> Ingredients { get; set; } = new();
    public string StorageRequirements { get; set; } = string.Empty;
    public string ProcessingMethod { get; set; } = string.Empty;
    public bool CrossContaminationChecked { get; set; }
    public string VerificationNotes { get; set; } = string.Empty;
}

public enum InspectionResult
{
    Pass,
    PassWithObservations,
    MinorDefects,
    MajorDefects,
    Critical,
    Reject
}

public enum CheckpointPriority
{
    Low,
    Medium,
    High,
    Critical
}