namespace IslamicPOS.Core.Models.Wholesale
{
    public class InspectionSummary
    {
        public int Id { get; set; }
        public DateTime InspectionDate { get; set; }
        public string InspectorId { get; set; } = string.Empty;
        public string BatchNumber { get; set; } = string.Empty;
        public bool PassedInspection { get; set; }
        public string Notes { get; set; } = string.Empty;
        public List<InspectionItem> Items { get; set; } = new();
    }

    public class InspectionItem
    {
        public int Id { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public string ChecklistItem { get; set; } = string.Empty;
        public bool Passed { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}