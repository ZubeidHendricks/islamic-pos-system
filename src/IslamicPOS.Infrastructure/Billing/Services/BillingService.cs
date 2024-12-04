using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IslamicPOS.Core.Billing.Interfaces;
using IslamicPOS.Core.Billing.Models;
using IslamicPOS.Core.MultiTenant.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Billing.Services
{
    public class BillingService : IBillingService
    {
        private readonly BillingDbContext _context;
        private readonly ITenantManager _tenantManager;
        private readonly IPaymentProcessor _paymentProcessor;

        public BillingService(
            BillingDbContext context,
            ITenantManager tenantManager,
            IPaymentProcessor paymentProcessor)
        {
            _context = context;
            _tenantManager = tenantManager;
            _paymentProcessor = paymentProcessor;
        }

        public async Task<Invoice> GenerateInvoiceAsync(Guid tenantId)
        {
            var tenant = await _tenantManager.GetTenantAsync(tenantId);
            if (tenant == null) throw new Exception("Tenant not found");

            var plan = await GetPlanDetailsAsync(tenant.Tier);
            var usage = await _tenantManager.GetTenantUsageAsync(tenantId);

            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                InvoiceNumber = GenerateInvoiceNumber(),
                IssueDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                Status = InvoiceStatus.Draft,
                LineItems = new List<InvoiceLineItem>()
            };

            // Add base subscription
            invoice.LineItems.Add(new InvoiceLineItem
            {
                Description = $"{plan.Name} Subscription",
                Quantity = 1,
                UnitPrice = plan.MonthlyPrice,
                Total = plan.MonthlyPrice,
                Type = "Subscription"
            });

            // Add addons
            foreach (var addon in tenant.Addons)
            {
                var addonPrice = await CalculateAddonPrice(addon);
                invoice.LineItems.Add(new InvoiceLineItem
                {
                    Description = $"Addon: {addon.Name}",
                    Quantity = 1,
                    UnitPrice = addonPrice,
                    Total = addonPrice,
                    Type = "Addon"
                });
            }

            // Calculate overage charges
            var overageCharges = await CalculateOverageCharges(tenant, usage, plan);
            if (overageCharges.Any())
            {
                invoice.LineItems.AddRange(overageCharges);
            }

            // Calculate totals
            invoice.SubTotal = invoice.LineItems.Sum(x => x.Total);
            invoice.Tax = await CalculateTax(invoice.SubTotal, tenant);
            invoice.Total = invoice.SubTotal + invoice.Tax;

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task<Invoice> GetInvoiceAsync(Guid invoiceId)
        {
            return await _context.Invoices
                .Include(i => i.LineItems)
                .Include(i => i.Payments)
                .FirstOrDefaultAsync(i => i.Id == invoiceId);
        }

        public async Task<List<Invoice>> GetTenantInvoicesAsync(Guid tenantId)
        {
            return await _context.Invoices
                .Include(i => i.LineItems)
                .Include(i => i.Payments)
                .Where(i => i.TenantId == tenantId)
                .OrderByDescending(i => i.IssueDate)
                .ToListAsync();
        }

        public async Task<InvoicePayment> ProcessPaymentAsync(Guid invoiceId, PaymentRequest request)
        {
            var invoice = await GetInvoiceAsync(invoiceId);
            if (invoice == null) throw new Exception("Invoice not found");

            // Process payment through payment processor
            var paymentResult = await _paymentProcessor.ProcessPaymentAsync(request);

            var payment = new InvoicePayment
            {
                Id = Guid.NewGuid(),
                PaymentDate = DateTime.UtcNow,
                Amount = request.Amount,
                TransactionId = paymentResult.TransactionId,
                Method = request.Method,
                Status = paymentResult.Success ? PaymentStatus.Completed : PaymentStatus.Failed
            };

            invoice.Payments.Add(payment);

            // Update invoice status
            if (paymentResult.Success)
            {
                var totalPaid = invoice.Payments.Where(p => p.Status == PaymentStatus.Completed)
                    .Sum(p => p.Amount);
                
                if (totalPaid >= invoice.Total)
                {
                    invoice.Status = InvoiceStatus.Paid;
                }
            }

            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<bool> CancelSubscriptionAsync(Guid tenantId)
        {
            var tenant = await _tenantManager.GetTenantAsync(tenantId);
            if (tenant == null) return false;

            // Generate final invoice
            await GenerateInvoiceAsync(tenantId);

            // Update tenant subscription
            tenant.SubscriptionEnd = DateTime.UtcNow;
            await _tenantManager.UpdateTenantAsync(tenantId, new TenantUpdate
            {
                IsActive = false
            });

            return true;
        }

        private string GenerateInvoiceNumber()
        {
            return $"INV-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8)}".ToUpper();
        }

        private async Task<decimal> CalculateTax(decimal amount, Tenant tenant)
        {
            // Implement tax calculation based on tenant location
            return amount * 0.15m; // Example: 15% tax
        }

        private async Task<decimal> CalculateAddonPrice(AddonFeature addon)
        {
            // Implement addon pricing logic
            return 10.00m; // Example fixed addon price
        }

        private async Task<List<InvoiceLineItem>> CalculateOverageCharges(
            Tenant tenant, TenantUsage usage, BillingPlan plan)
        {
            var overageItems = new List<InvoiceLineItem>();

            // Check user overage
            if (usage.CurrentUsers > tenant.Limits.MaxUsers)
            {
                var extraUsers = usage.CurrentUsers - tenant.Limits.MaxUsers;
                overageItems.Add(new InvoiceLineItem
                {
                    Description = $"Extra Users ({extraUsers})",
                    Quantity = extraUsers,
                    UnitPrice = 10.00m, // Example: $10 per extra user
                    Total = extraUsers * 10.00m,
                    Type = "Overage"
                });
            }

            // Add more overage calculations as needed

            return overageItems;
        }
    }
}