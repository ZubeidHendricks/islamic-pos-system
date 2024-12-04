using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations
{
    public partial class AddTenantAndBilling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Tenants table
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 200, nullable: false),
                    Domain = table.Column<string>(maxLength: 100, nullable: false),
                    ConnectionString = table.Column<string>(maxLength: 500, nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    SubscriptionStart = table.Column<DateTime>(nullable: false),
                    SubscriptionEnd = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Settings_TimeZone = table.Column<string>(maxLength: 50, nullable: true),
                    Settings_Currency = table.Column<string>(maxLength: 10, nullable: true),
                    Settings_Language = table.Column<string>(maxLength: 10, nullable: true),
                    Settings_LogoUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Settings_CustomSettings = table.Column<string>(type: "jsonb", nullable: true),
                    Limits_MaxUsers = table.Column<int>(nullable: true),
                    Limits_MaxLocations = table.Column<int>(nullable: true),
                    Limits_MaxTransactionsPerMonth = table.Column<int>(nullable: true),
                    Limits_MaxProductsPerLocation = table.Column<int>(nullable: true),
                    Limits_StorageLimit = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            // Create BillingPlans table
            migrationBuilder.CreateTable(
                name: "BillingPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    MonthlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnnualPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingPlans", x => x.Id);
                });

            // Create Invoices table
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TenantId = table.Column<Guid>(nullable: false),
                    InvoiceNumber = table.Column<string>(maxLength: 50, nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create indexes
            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Domain",
                table: "Tenants",
                column: "Domain",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TenantId",
                table: "Invoices",
                column: "TenantId");

            // Add seed data
            migrationBuilder.InsertData(
                table: "BillingPlans",
                columns: new[] { "Id", "Name", "Description", "MonthlyPrice", "AnnualPrice", "Tier", "IsActive" },
                values: new object[,]
                {
                    {
                        Guid.NewGuid(),
                        "Basic",
                        "For small businesses",
                        49.99m,
                        499.99m,
                        0, // Basic tier
                        true
                    },
                    {
                        Guid.NewGuid(),
                        "Professional",
                        "For growing businesses",
                        99.99m,
                        999.99m,
                        1, // Professional tier
                        true
                    },
                    {
                        Guid.NewGuid(),
                        "Enterprise",
                        "For large businesses",
                        199.99m,
                        1999.99m,
                        2, // Enterprise tier
                        true
                    }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Invoices");
            migrationBuilder.DropTable(name: "BillingPlans");
            migrationBuilder.DropTable(name: "Tenants");
        }
    }
}