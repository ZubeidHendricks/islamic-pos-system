using IslamicPOS.Core.Models.Wholesale;

namespace IslamicPOS.Core.Services.Wholesale;

public interface IQualityControlService
{
    Task<QualityControl> InitiateInspection(VendorProduct product, int batchNumber);
    Task<QualityControl> CompleteInspection(QualityControl inspection);
    Task<bool> ValidateHalalCompliance(Guid productId, string certificateNumber);
    Task<List<QualityCheckpoint>> GenerateCheckpoints(VendorProduct product, VendorScore score);
    Task<bool> RequiresManagerReview(QualityControl inspection);
    Task<QualityControl> ApproveInspection(Guid inspectionId, string managerId, string notes);
    Task<List<QualityControl>> GetPendingInspections();
    Task<List<QualityControl>> GetInspectionsByProduct(Guid productId);
    Task<InspectionSummary> GetInspectionSummary(DateTime startDate, DateTime endDate);
}