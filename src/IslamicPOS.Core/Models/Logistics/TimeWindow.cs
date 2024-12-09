namespace IslamicPOS.Core.Models.Logistics
{
    public class TimeWindow
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DayOfWeek[] AvailableDays { get; set; } = Array.Empty<DayOfWeek>();
    }
}