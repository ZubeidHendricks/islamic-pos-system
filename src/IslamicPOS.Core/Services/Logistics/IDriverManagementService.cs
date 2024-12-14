namespace IslamicPOS.Core.Services.Logistics
{
    public interface IDriverManagementService
    {
        Task<IEnumerable<DriverSchedule>> GetSchedule(DateTime date);
        Task<DriverPerformance> GetPerformance(string driverId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<DriverAlert>> GetAlerts();
    }

    public class DriverSchedule
    {
        public string DriverId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<string> DeliveryOrderIds { get; set; } = new();
    }

    public class DriverPerformance
    {
        public string DriverId { get; set; }
        public int DeliveriesCompleted { get; set; }
        public int DeliveriesFailed { get; set; }
        public decimal OnTimePercentage { get; set; }
    }

    public class DriverAlert
    {
        public string DriverId { get; set; }
        public string AlertType { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}