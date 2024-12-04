using System;
using System.Collections.Generic;

namespace IslamicPOS.Core.Logistics.Models
{
    public class DeliveryPoint
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public TimeWindow DeliveryWindow { get; set; }
        public Dictionary<string, object> SpecialRequirements { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public bool RequiresHalalSegregation { get; set; }
        public Guid OrderId { get; set; }
    }

    public class TimeWindow
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}