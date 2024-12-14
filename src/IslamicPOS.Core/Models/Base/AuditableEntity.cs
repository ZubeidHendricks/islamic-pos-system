namespace IslamicPOS.Core.Models.Base
{
    public abstract class AuditableEntity : EntityBase
    {
        public string AuditLog { get; set; }
        public int Version { get; set; }
    }
}