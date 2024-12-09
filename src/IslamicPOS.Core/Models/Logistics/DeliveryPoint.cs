namespace IslamicPOS.Core.Models.Logistics
{
    public class DeliveryPoint
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public TimeWindow DeliveryWindow { get; set; } = new();
    }
}