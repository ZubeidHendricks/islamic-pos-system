using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Inventory;
using MediatR;
using IslamicPOS.Application.Common.Interfaces;

namespace IslamicPOS.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Product>
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string SKU { get; init; } = string.Empty;
    public decimal Price { get; init; } // Changed from Money to decimal
    public bool IsHalal { get; init; }
    public string HalalCertification { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public int StockQuantity { get; init; } = 0;
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name,
            request.Description,
            request.SKU,
            request.Price,
            request.IsHalal,
            request.HalalCertification,
            request.Category,
            request.StockQuantity);

        _context.Products.Add(product);
        await _context.SaveChangesAsync(cancellationToken);

        return product;
    }
}