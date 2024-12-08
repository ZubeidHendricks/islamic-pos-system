namespace IslamicPOS.Core.Models.Delivery;

public class DeliveryPoint
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string ContactPerson { get; set; }
    public string ContactPhone { get; set; }
    public TimeWindow DeliveryWindow { get; set; }
    public bool RequiresHalalCertified { get; set; }
    public string SpecialInstructions { get; set; }
}