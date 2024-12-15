using IslamicPOS.Application.Common.Interfaces;
using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Finance.Models;
using MediatR;

namespace IslamicPOS.Application.Finance.Commands.CreateMudarabahContract;

public record CreateMudarabahContractCommand : IRequest<MudarabahContract>
{
    public Money InvestmentAmount { get; init; } = Money.Zero();
    public decimal ProfitSharingRatio { get; init; }
    public string InvestorId { get; init; } = string.Empty;
    public string BusinessId { get; init; } = string.Empty;
    public string Terms { get; init; } = string.Empty;
}

public class CreateMudarabahContractCommandHandler : IRequestHandler<CreateMudarabahContractCommand, MudarabahContract>
{
    private readonly IApplicationDbContext _context;

    public CreateMudarabahContractCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<MudarabahContract> Handle(CreateMudarabahContractCommand request, CancellationToken cancellationToken)
    {
        var contract = new MudarabahContract(
            request.InvestmentAmount,
            request.ProfitSharingRatio,
            DateTime.UtcNow,
            request.InvestorId,
            request.BusinessId,
            request.Terms);

        _context.MudarabahContracts.Add(contract);
        await _context.SaveChangesAsync(cancellationToken);

        return contract;
    }
}

