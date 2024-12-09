using IslamicPOS.Application.Common.Interfaces;
using IslamicPOS.Application.Common.Models;
using IslamicPOS.Domain.Finance;
using IslamicPOS.Domain.ValueObjects;
using MediatR;

namespace IslamicPOS.Application.Finance.Queries.GetZakaatCalculation
{
    public record GetZakaatCalculationQuery : IRequest<Result<ZakaatCalculation>>
    {
        public decimal CashOnHand { get; init; }
        public decimal BankBalance { get; init; }
        public decimal GoldValue { get; init; }
        public decimal SilverValue { get; init; }
        public decimal StockValue { get; init; }
        public decimal BusinessAssets { get; init; }
        public decimal Investments { get; init; }
        public decimal Debts { get; init; }
        public string Currency { get; init; } = "USD";
    }

    public class GetZakaatCalculationQueryHandler : IRequestHandler<GetZakaatCalculationQuery, Result<ZakaatCalculation>>
    {
        private readonly IApplicationDbContext _context;

        public GetZakaatCalculationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ZakaatCalculation>> Handle(GetZakaatCalculationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalAssets = new Money(
                    request.CashOnHand +
                    request.BankBalance +
                    request.GoldValue +
                    request.SilverValue +
                    request.StockValue +
                    request.BusinessAssets +
                    request.Investments,
                    request.Currency);

                var totalLiabilities = new Money(request.Debts, request.Currency);
                var netWorth = totalAssets.Subtract(totalLiabilities);

                var financeOptions = await _context.FinanceOptions
                    .OrderByDescending(o => o.Created)
                    .FirstAsync(cancellationToken);

                var calculation = new ZakaatCalculation
                {
                    TotalWealth = netWorth,
                    NisabThreshold = financeOptions.NisabThreshold,
                    ZakaatAmount = netWorth.Multiply(financeOptions.ZakaatRate),
                    IsZakaatDue = netWorth.Amount >= financeOptions.NisabThreshold.Amount,
                    CalculationDate = DateTime.UtcNow
                };

                _context.ZakaatCalculations.Add(calculation);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<ZakaatCalculation>.Success(calculation);
            }
            catch (Exception ex)
            {
                return Result<ZakaatCalculation>.Failure($"Failed to calculate Zakaat: {ex.Message}");
            }
        }
    }
}