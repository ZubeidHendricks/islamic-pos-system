using System;

namespace IslamicPOS.Core.Ticketing.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string TicketNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiryDate { get; set; }
        public TicketStatus Status { get; set; }
        public string QRCode { get; set; }
        public string CollectionLocation { get; set; }
        public DateTime? CollectedAt { get; set; }
        public string CollectedBy { get; set; }
    }

    public enum TicketStatus
    {
        Active,
        Used,
        Expired,
        Cancelled
    }
}