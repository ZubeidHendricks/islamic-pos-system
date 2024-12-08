namespace IslamicPOS.Core.Models.Delivery
{
    public class DeliveryPoint
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string DeliveryInstructions { get; set; }
    }
}