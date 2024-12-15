namespace IslamicPOS.Domain.Common
{
    public abstract class AuditableEntity : Entity
    {
        public DateTime Created { get; set; }
        public new string? CreatedBy { get; set; } // Use 'new' keyword
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}