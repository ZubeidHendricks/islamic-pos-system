using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Sales;
using IslamicPOS.Domain.Finance.Models;
using MediatR;
using IslamicPOS.Application.Common.Interfaces;

namespace IslamicPOS.Application.Sales.Commands.CreateSale;

public record CreateSaleCommand : IRequest<Sale>
{
    public required string CustomerId { get; init; }
    public required List<SaleItem> Items { get; init; }
    public required PaymentMethod PaymentMethod { get; init; }
}

public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Sale>
{
    private readonly IApplicationDbContext _context;

    public CreateSaleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Sale> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = new Sale(request.CustomerId, request.Items);
        
        _context.Sales.Add(sale);
        await _context.SaveChangesAsync(cancellationToken);

        return sale;
    }
}
