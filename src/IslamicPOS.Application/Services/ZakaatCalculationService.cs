using IslamicPOS.Domain.Finance;
using IslamicPOS.Domain.ValueObjects;
using IslamicPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Application.Services
{
    public class ZakaatCalculationService
    {
        private readonly ApplicationDbContext _context;

        public ZakaatCalculationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ZakaatCalculation> CalculateZakaat(ZakaatInput input)
        {
            var financeOptions = await _context.FinanceOptions
                .OrderByDescending(o => o.Created)
                .FirstAsync();

            var totalWealth = new Money(
                input.CashOnHand +
                input.BankBalance +
                input.GoldValue +
                input.SilverValue +
                input.StockValue +
                input.BusinessAssets +
                input.Investments -
                input.Debts,
                input.Currency);

            var calculation = new ZakaatCalculation
            {
                TotalWealth = totalWealth,
                NisabThreshold = financeOptions.NisabThreshold,
                ZakaatAmount = totalWealth.Multiply(financeOptions.ZakaatRate),
                IsZakaatDue = totalWealth.Amount >= financeOptions.NisabThreshold.Amount,
                CalculationDate = DateTime.UtcNow
            };

            _context.ZakaatCalculations.Add(calculation);
            await _context.SaveChangesAsync();

            return calculation;
        }
    }

    public class ZakaatInput
    {
        public decimal CashOnHand { get; set; }
        public decimal BankBalance { get; set; }
        public decimal GoldValue { get; set; }
        public decimal SilverValue { get; set; }
        public decimal StockValue { get; set; }
        public decimal BusinessAssets { get; set; }
        public decimal Investments { get; set; }
        public decimal Debts { get; set; }
        public string Currency { get; set; } = "USD";
    }
}