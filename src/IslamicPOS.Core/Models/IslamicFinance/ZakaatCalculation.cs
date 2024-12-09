namespace IslamicPOS.Core.Models.IslamicFinance
{
    public class ZakaatCalculation
    {
        public decimal TotalWealth { get; set; }
        public decimal NisabThreshold { get; set; }
        public decimal ZakaatAmount { get; set; }
        public bool IsZakaatDue { get; set; }
        public DateTime CalculationDate { get; set; }
    }
}