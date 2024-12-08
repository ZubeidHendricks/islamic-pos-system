using System;
using System.Collections.Generic;

namespace IslamicPOS.Core.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string TransactionType { get; set; } // Sale, Purchase, Return, etc.
        public string Status { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<TransactionItem> Items { get; set; }
        public string PaymentMethod { get; set; }
        public string ReferenceNumber { get; set; }
        public string Notes { get; set; }
    }

    public class TransactionItem
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public string Notes { get; set; }
    }
}