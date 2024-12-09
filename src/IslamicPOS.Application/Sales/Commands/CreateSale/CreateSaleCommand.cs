using IslamicPOS.Application.Common.Interfaces;
using IslamicPOS.Application.Common.Models;
using IslamicPOS.Domain.Sales;
using IslamicPOS.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Application.Sales.Commands.CreateSale
{
    public record CreateSaleCommand : IRequest<Result<int>>
    {
        public string CustomerName { get; init; } = string.Empty;
        public string CustomerPhone { get; init; } = string.Empty;
        public PaymentMethod PaymentMethod { get; init; }
        public List<SaleItemDto> Items { get; init; } = new();
    }

    public record SaleItemDto
    {
        public int ProductId { get; init; }
        public int Quantity { get; init; }
    }

    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeService _dateTime;

        public CreateSaleCommandHandler(IApplicationDbContext context, IDateTimeService dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }

        public async Task<Result<int>> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _context.Products
                    .Where(p => request.Items.Select(i => i.ProductId).Contains(p.Id))
                    .ToDictionaryAsync(p => p.Id, cancellationToken);

                if (products.Count != request.Items.Count)
                    return Result<int>.Failure("One or more products not found");

                var invoiceNumber = GenerateInvoiceNumber();
                var sale = Sale.Create(invoiceNumber, request.CustomerName, request.CustomerPhone, request.PaymentMethod);

                foreach (var item in request.Items)
                {
                    var product = products[item.ProductId];
                    sale.AddItem(product, item.Quantity, product.Price);
                }

                _context.Sales.Add(sale);
                await _context.SaveChangesAsync(cancellationToken);

                return Result<int>.Success(sale.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Failure($"Failed to create sale: {ex.Message}");
            }
        }

        private string GenerateInvoiceNumber()
        {
            return $"INV-{_dateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
    }
}