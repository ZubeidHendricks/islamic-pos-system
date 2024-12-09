using IslamicPOS.Application.Common.Interfaces;
using IslamicPOS.Application.Common.Models;
using IslamicPOS.Domain.Inventory;
using IslamicPOS.Domain.ValueObjects;
using MediatR;

namespace IslamicPOS.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<Result<int>>
    {
        public string Name { get; init; } = string.Empty;
        public string SKU { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public string Currency { get; init; } = string.Empty;
        public int MinimumStockLevel { get; init; }
        public bool IsHalal { get; init; }
        public string HalalCertification { get; init; } = string.Empty;
        public int CategoryId { get; init; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;

        public CreateProductCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _context.Categories.FindAsync(new object[] { request.CategoryId }, cancellationToken);
                if (category == null)
                    return Result<int>.Failure($"Category with ID {request.CategoryId} not found");

                var price = new Money(request.Price, request.Currency);
                var product = Product.Create(
                    request.Name,
                    request.SKU,
                    request.Description,
                    price,
                    request.MinimumStockLevel,
                    request.IsHalal,
                    request.HalalCertification,
                    category);

                _context.Products.Add(product);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<int>.Success(product.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure($"Failed to create product: {ex.Message}");
            }
        }
    }
}