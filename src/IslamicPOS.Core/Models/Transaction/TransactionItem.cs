using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Transaction
{
    public class TransactionItem
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public int ProductId { get; set; }
        public Product.Product Product { get; set; }
        public int Quantity { get; set; }
        public Money UnitPrice { get; set; }
        public Money Subtotal => Money.Create(UnitPrice.Amount * Quantity, UnitPrice.Currency);
    }
}