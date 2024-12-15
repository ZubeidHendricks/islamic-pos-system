using IslamicPOS.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using IslamicPOS.Domain.Finance.Models;
using IslamicPOS.Domain.Sales;
using IslamicPOS.Domain.Inventory;

namespace IslamicPOS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Sale> Sales { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<MudarabahContract> MudarabahContracts { get; }
    DbSet<ZakaatCalculation> ZakaatCalculations { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

