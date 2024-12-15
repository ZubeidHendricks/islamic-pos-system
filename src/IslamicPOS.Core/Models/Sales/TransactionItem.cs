using IslamicPOS.Core.Common;
using IslamicPOS.Core.Models.Common;
using IslamicPOS.Core.Models.Products;

namespace IslamicPOS.Core.Models.Sales;

public class TransactionItem : Entity
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public Money UnitPrice { get; set; } = Money.Zero();
    public Money Subtotal => new(UnitPrice.Amount * Quantity, UnitPrice.Currency);
    public string Notes { get; set; } = string.Empty;
}
