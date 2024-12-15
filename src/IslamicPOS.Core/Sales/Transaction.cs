using IslamicPOS.Core.Common;

namespace IslamicPOS.Core.Sales;

public class Transaction : Entity
{
    private readonly List<TransactionItem> _items = new();
    
    public IReadOnlyCollection<TransactionItem> Items => _items.AsReadOnly();
    public Money Total { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public TransactionStatus Status { get; private set; }
    public DateTime TransactionDate { get; private set; }
    
    private Transaction(PaymentMethod paymentMethod)
    {
        PaymentMethod = paymentMethod;
        Status = TransactionStatus.Created;
        TransactionDate = DateTime.UtcNow;
        Total = Money.Create(0);
    }
    
    public static Transaction Create(PaymentMethod paymentMethod)
    {
        return new Transaction(paymentMethod);
    }
    
    public void AddItem(Product product, int quantity)
    {
        if (Status != TransactionStatus.Created)
            throw new InvalidOperationException("Cannot modify a completed transaction");
            
        var item = new TransactionItem(product, quantity);
        _items.Add(item);
        RecalculateTotal();
    }
    
    private void RecalculateTotal()
    {
        var total = _items.Aggregate(
            Money.Create(0),
            (current, item) => current.Add(item.SubTotal));
            
        Total = total;
    }
    
    public void Complete()
    {
        if (Status != TransactionStatus.Created)
            throw new InvalidOperationException("Transaction already completed");
            
        Status = TransactionStatus.Completed;
    }
}