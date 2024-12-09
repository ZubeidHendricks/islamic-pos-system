using IslamicPOS.Application.Common.Models;
using IslamicPOS.Domain.Finance;
using MediatR;

namespace IslamicPOS.Application.Finance.Commands.CalculateZakaat
{
    public record CalculateZakaatCommand : IRequest<Result<ZakaatCalculation>>
    {
        public ZakaatInput Input { get; init; } = null!;
    }

    public class CalculateZakaatCommandHandler : IRequestHandler<CalculateZakaatCommand, Result<ZakaatCalculation>>
    {
        private readonly IZakaatService _zakaatService;

        public CalculateZakaatCommandHandler(IZakaatService zakaatService)
        {
            _zakaatService = zakaatService;
        }

        public async Task<Result<ZakaatCalculation>> Handle(CalculateZakaatCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var calculation = _zakaatService.CalculateZakaat(request.Input);
                return Result<ZakaatCalculation>.Success(calculation);
            }
            catch (Exception ex)
            {
                return Result<ZakaatCalculation>.Failure($"Failed to calculate Zakaat: {ex.Message}");
            }
        }
    }
}