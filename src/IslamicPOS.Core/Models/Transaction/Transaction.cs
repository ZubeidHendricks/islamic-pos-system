using IslamicPOS.Core.Models.Base;
using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Transaction
{
    public class Transaction : AuditableEntity
    {
        public Guid TransactionNumber { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public List<TransactionItem> Items { get; private set; }
        public Money TotalAmount { get; private set; }
        public Money ZakatAmount { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public TransactionStatus Status { get; private set; }
        public bool IsHalalCompliant { get; private set; }
        public string ComplianceNotes { get; private set; }

        private Transaction() {} // For EF Core

        public Transaction(List<TransactionItem> items, PaymentMethod paymentMethod)
        {
            TransactionNumber = Guid.NewGuid();
            TransactionDate = DateTime.UtcNow;
            Items = items;
            PaymentMethod = paymentMethod;
            Status = TransactionStatus.Pending;
            CalculateTotals();
        }

        private void CalculateTotals()
        {
            var total = Items.Sum(i => i.Subtotal.Amount);
            TotalAmount = Money.Create(total);
        }

        public void Complete()
        {
            Status = TransactionStatus.Completed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetZakat(Money zakatAmount)
        {
            ZakatAmount = zakatAmount;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsHalalCompliant(string notes = null)
        {
            IsHalalCompliant = true;
            ComplianceNotes = notes;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}