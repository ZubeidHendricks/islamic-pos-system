using System;
using System.Collections.Generic;

namespace IslamicPOS.Core.Logistics.Models
{
    public class OptimizedRoute
    {
        public Guid RouteId { get; set; }
        public List<DeliveryPoint> Waypoints { get; set; }
        public TimeSpan EstimatedDuration { get; set; }
        public double TotalDistance { get; set; }
        public VehicleType RecommendedVehicleType { get; set; }
        public Dictionary<string, object> SpecialHandlingInstructions { get; set; }
        public double TotalWeight { get; set; }
        public double TotalVolume { get; set; }
        public bool RequiresHalalSegregation { get; set; }
        public RouteStatus Status { get; set; }
    }

    public enum RouteStatus
    {
        Planned,
        InProgress,
        Completed,
        Cancelled
    }

    public enum VehicleType
    {
        Small,
        Medium,
        Large,
        RefrigeratedSmall,
        RefrigeratedLarge,
        HalalCertified
    }
}