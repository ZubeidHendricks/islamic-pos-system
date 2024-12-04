using System;
using System.Collections.Generic;

namespace IslamicPOS.Core.Billing.Models
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public InvoiceStatus Status { get; set; }
        public List<InvoiceLineItem> LineItems { get; set; }
        public List<InvoicePayment> Payments { get; set; }
    }

    public class InvoiceLineItem
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public string Type { get; set; } // Subscription, Addon, Overage, etc.
    }

    public class InvoicePayment
    {
        public Guid Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string TransactionId { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
    }

    public enum InvoiceStatus
    {
        Draft,
        Sent,
        Paid,
        Overdue,
        Cancelled,
        Refunded
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}