namespace IslamicPOS.Core.Models.Common;

public abstract class AuditableEntity : Entity
{
    public string? Notes { get; set; }
    public string? AuditTrail { get; set; }
    public DateTime? LastAuditedAt { get; set; }
    public string? LastAuditedBy { get; set; }
}