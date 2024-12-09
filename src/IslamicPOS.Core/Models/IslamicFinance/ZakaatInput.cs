namespace IslamicPOS.Core.Models.IslamicFinance
{
    public class ZakaatInput
    {
        public decimal CashOnHand { get; set; }
        public decimal BankBalance { get; set; }
        public decimal GoldValue { get; set; }
        public decimal SilverValue { get; set; }
        public decimal StockValue { get; set; }
        public decimal BusinessAssets { get; set; }
        public decimal Debts { get; set; }
        public decimal Investments { get; set; }
    }
}