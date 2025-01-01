using IslamicPOS.Core.Common;
using IslamicPOS.Core.Models.Products;

namespace IslamicPOS.Core.Models.Sales;

public class TransactionItem : Entity
{
    public Guid TransactionId { get; set; }
    public Transaction? Transaction { get; set; }
    
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public string Notes { get; set; } = string.Empty;
}