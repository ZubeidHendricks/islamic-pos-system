using IslamicPOS.Domain.Finance;
using IslamicPOS.Domain.Inventory;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; }
        DbSet<ProductCategory> Categories { get; }
        DbSet<MudarabahContract> MudarabahContracts { get; }
        DbSet<ZakaatCalculation> ZakaatCalculations { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}