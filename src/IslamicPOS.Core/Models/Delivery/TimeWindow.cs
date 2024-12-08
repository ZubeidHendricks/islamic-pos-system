namespace IslamicPOS.Core.Models.Delivery;

public class TimeWindow
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    
    public bool IsWithinWindow(DateTime time) =>
        time >= Start && time <= End;
    
    public TimeSpan Duration =>
        End - Start;
}