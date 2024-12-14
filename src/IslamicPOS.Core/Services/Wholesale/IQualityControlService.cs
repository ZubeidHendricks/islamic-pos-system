namespace IslamicPOS.Core.Services.Wholesale
{
    public interface IQualityControlService
    {
        Task<bool> ValidateProduct(string productId);
        Task<InspectionSummary> GetInspectionSummary(string productId);
    }

    public class InspectionSummary
    {
        public string ProductId { get; set; }
        public bool PassedInspection { get; set; }
        public string InspectorId { get; set; }
        public DateTime InspectionDate { get; set; }
        public List<string> Notes { get; set; } = new();
    }
}