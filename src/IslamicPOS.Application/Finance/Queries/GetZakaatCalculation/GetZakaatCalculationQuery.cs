using IslamicPOS.Application.Common.Interfaces;
using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Finance.Models;
using MediatR;

namespace IslamicPOS.Application.Finance.Queries.GetZakaatCalculation;

public record GetZakaatCalculationQuery(Guid Id) : IRequest<ZakaatCalculation>;

public class GetZakaatCalculationQueryHandler : IRequestHandler<GetZakaatCalculationQuery, ZakaatCalculation>
{
    private readonly IApplicationDbContext _context;

    public GetZakaatCalculationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ZakaatCalculation> Handle(GetZakaatCalculationQuery request, CancellationToken cancellationToken)
    {
        var zakaatCalculation = await _context.ZakaatCalculations.FindAsync(new object[] { request.Id }, cancellationToken);
        
        if (zakaatCalculation == null)
            throw new Exception($"ZakaatCalculation with ID {request.Id} not found.");

        return zakaatCalculation;
    }
}

