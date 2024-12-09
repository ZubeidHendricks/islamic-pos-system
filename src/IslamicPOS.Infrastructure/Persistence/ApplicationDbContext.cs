using IslamicPOS.Application.Common.Interfaces;
using IslamicPOS.Domain.Common;
using IslamicPOS.Domain.Finance;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IslamicPOS.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<IslamicFinanceOptions> FinanceOptions => Set<IslamicFinanceOptions>();
        public DbSet<MudarabahContract> MudarabahContracts => Set<MudarabahContract>();
        public DbSet<MusharakahContract> MusharakahContracts => Set<MusharakahContract>();
        public DbSet<ZakaatCalculation> ZakaatCalculations => Set<ZakaatCalculation>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}