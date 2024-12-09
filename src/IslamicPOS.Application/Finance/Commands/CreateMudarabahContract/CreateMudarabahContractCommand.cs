using IslamicPOS.Application.Common.Interfaces;
using IslamicPOS.Application.Common.Models;
using IslamicPOS.Domain.Finance;
using IslamicPOS.Domain.ValueObjects;
using MediatR;

namespace IslamicPOS.Application.Finance.Commands.CreateMudarabahContract
{
    public record CreateMudarabahContractCommand : IRequest<Result<int>>
    {
        public string ContractNumber { get; init; } = string.Empty;
        public string RabbulMaalId { get; init; } = string.Empty;
        public string MudaribId { get; init; } = string.Empty;
        public decimal InvestedAmount { get; init; }
        public string Currency { get; init; } = string.Empty;
        public decimal ProfitSharingRatio { get; init; }
        public DateTime StartDate { get; init; }
    }

    public class CreateMudarabahContractCommandHandler : IRequestHandler<CreateMudarabahContractCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;

        public CreateMudarabahContractCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(CreateMudarabahContractCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var investedCapital = new Money(request.InvestedAmount, request.Currency);
                var contract = MudarabahContract.Create(
                    request.ContractNumber,
                    request.RabbulMaalId,
                    request.MudaribId,
                    investedCapital,
                    request.ProfitSharingRatio,
                    request.StartDate);

                _context.MudarabahContracts.Add(contract);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<int>.Success(contract.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure($"Failed to create Mudarabah contract: {ex.Message}");
            }
        }
    }
}