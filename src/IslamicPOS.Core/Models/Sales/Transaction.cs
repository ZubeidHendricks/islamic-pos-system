using IslamicPOS.Core.Common;
using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.Core.Models.Sales;

public class Transaction : Entity
{
    public DateTime TransactionDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethodId { get; set; } = string.Empty;
    public PaymentMethod? PaymentMethod { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    
    public ICollection<TransactionItem> Items { get; set; } = new List<TransactionItem>();
}