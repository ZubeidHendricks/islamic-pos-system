using System;
using IslamicPOS.Core.MultiTenant.Models;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Data.MultiTenant
{
    public class TenantDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<BillingPlan> BillingPlans { get; set; }
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CompanyName).IsRequired();
                entity.Property(e => e.Domain).IsRequired();
                entity.HasIndex(e => e.Domain).IsUnique();

                entity.OwnsOne(e => e.Settings);
                entity.OwnsOne(e => e.Limits);

                entity.HasMany(e => e.Addons)
                    .WithOne()
                    .HasForeignKey("TenantId");
            });

            modelBuilder.Entity<BillingPlan>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.MonthlyPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.AnnualPrice).HasColumnType("decimal(18,2)");

                entity.HasMany(e => e.Features)
                    .WithOne()
                    .HasForeignKey("BillingPlanId");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.InvoiceNumber).IsRequired();
                entity.Property(e => e.SubTotal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Tax).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Total).HasColumnType("decimal(18,2)");

                entity.HasMany(e => e.LineItems)
                    .WithOne()
                    .HasForeignKey("InvoiceId");

                entity.HasMany(e => e.Payments)
                    .WithOne()
                    .HasForeignKey("InvoiceId");
            });
        }
    }
}