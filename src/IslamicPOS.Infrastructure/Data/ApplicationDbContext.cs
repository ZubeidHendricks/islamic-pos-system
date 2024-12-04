using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IslamicPOS.Core.Models;
using IslamicPOS.Core.Models.InventoryManagement;
using IslamicPOS.Infrastructure.Data.Configurations;

namespace IslamicPOS.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionItem> TransactionItems { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply entity configurations
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new TransactionConfiguration());
        builder.ApplyConfiguration(new TransactionItemConfiguration());
        builder.ApplyConfiguration(new StockMovementConfiguration());
        builder.ApplyConfiguration(new PurchaseOrderConfiguration());
        builder.ApplyConfiguration(new PurchaseOrderItemConfiguration());
        builder.ApplyConfiguration(new SupplierConfiguration());
        builder.ApplyConfiguration(new ProductCategoryConfiguration());

        // Add any additional configurations or seed data here
        ConfigureIdentityTables(builder);
        SeedInitialData(builder);
    }

    private void ConfigureIdentityTables(ModelBuilder builder)
    {
        // Customize identity table names and schema
        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
    }

    private void SeedInitialData(ModelBuilder builder)
    {
        // Seed default product categories
        builder.Entity<ProductCategory>().HasData(
            new ProductCategory { Id = 1, Name = "General Merchandise", IsActive = true, CreatedAt = DateTime.UtcNow },
            new ProductCategory { Id = 2, Name = "Food & Beverages", IsActive = true, CreatedAt = DateTime.UtcNow },
            new ProductCategory { Id = 3, Name = "Electronics", IsActive = true, CreatedAt = DateTime.UtcNow },
            new ProductCategory { Id = 4, Name = "Clothing", IsActive = true, CreatedAt = DateTime.UtcNow },
            new ProductCategory { Id = 5, Name = "Books", IsActive = true, CreatedAt = DateTime.UtcNow }
        );

        // Seed default roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER" },
            new IdentityRole { Id = "3", Name = "Cashier", NormalizedName = "CASHIER" }
        );
    }
}