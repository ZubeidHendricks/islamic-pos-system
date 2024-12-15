using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Sales;

public class TransactionItem : Entity
{
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Money SubTotal { get; private set; }
    
    public TransactionItem(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
            
        Product = product;
        Quantity = quantity;
        UnitPrice = product.Price;
        CalculateSubTotal();
    }
    
    private void CalculateSubTotal()
    {
        SubTotal = Money.Create(
            UnitPrice.Amount * Quantity,
            UnitPrice.Currency);
    }
}