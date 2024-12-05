using System;

namespace IslamicPOS.Core.Email.Models
{
    public class EmailQueueItem
    {
        public Guid Id { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
        public string TextContent { get; set; }
        public EmailStatus Status { get; set; }
        public int RetryCount { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime? NextRetryAt { get; set; }
        public Guid TenantId { get; set; }
        public EmailPriority Priority { get; set; }
    }

    public enum EmailStatus
    {
        Pending,
        Sending,
        Sent,
        Failed,
        Cancelled
    }

    public enum EmailPriority
    {
        Low = 0,
        Normal = 1,
        High = 2,
        Urgent = 3
    }
}