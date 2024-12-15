namespace IslamicPOS.Domain.Logistics;

public class Route
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}