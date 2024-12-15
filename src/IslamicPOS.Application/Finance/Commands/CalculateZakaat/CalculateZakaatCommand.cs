using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Finance.Models;
using IslamicPOS.Application.Common.Interfaces;
using MediatR;

namespace IslamicPOS.Application.Finance.Commands.CalculateZakaat;

public record CalculateZakaatCommand : IRequest<ZakaatCalculation>
{
    public Money Assets { get; init; } = Money.Zero();
    public Money Liabilities { get; init; } = Money.Zero();
    public Money BusinessAssets { get; init; } = Money.Zero();
    public Money Investments { get; init; } = Money.Zero();
}

public class CalculateZakaatCommandHandler : IRequestHandler<CalculateZakaatCommand, ZakaatCalculation>
{
    private readonly IApplicationDbContext _context;

    public CalculateZakaatCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ZakaatCalculation> Handle(CalculateZakaatCommand request, CancellationToken cancellationToken)
    {
        var zakaat = new ZakaatCalculation(
            request.Assets,
            request.Liabilities,
            request.BusinessAssets,
            request.Investments);

        _context.ZakaatCalculations.Add(zakaat);
        await _context.SaveChangesAsync(cancellationToken);
        return zakaat;
    }
}
