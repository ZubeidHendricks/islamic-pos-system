using IslamicPOS.Core.Common;
using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Sales;

public class Transaction : Entity
{
    public DateTime TransactionDate { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public List<TransactionItem> Items { get; set; } = new();
    public Money Total { get; set; } = Money.Zero();
    public string PaymentMethod { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
