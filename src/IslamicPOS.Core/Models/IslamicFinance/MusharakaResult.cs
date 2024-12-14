namespace IslamicPOS.Core.Models.IslamicFinance
{
    public class MusharakaResult
    {
        public decimal TotalCapital { get; set; }
        public decimal TotalProfit { get; set; }
        public List<PartnerShare> Partners { get; set; } = new();

        public bool IsLoss => TotalProfit < 0;
        public decimal ReturnOnInvestment => TotalCapital > 0 ? (TotalProfit / TotalCapital) * 100 : 0;
    }
}