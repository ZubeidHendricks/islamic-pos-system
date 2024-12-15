using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations;

public partial class InitialCreate : Migration
{
    protected override void UpInitialCreate(MigrationBuilder migrationBuilder)
    {
        // Mudarabah Contracts Table
        migrationBuilder.CreateTable(
            name: "MudarabahContracts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                InvestorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ContractAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ProfitSharePercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MudarabahContracts", x => x.Id);
            });

        // Zakat Calculations Table
        migrationBuilder.CreateTable(
            name: "ZakaatCalculations",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TotalAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                TotalLiabilities = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ZakaatableAssets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                ZakaatRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                ZakaatAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CalculationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ZakaatCalculations", x => x.Id);
            });

        // Create Indexes
        migrationBuilder.CreateIndex(
            name: "IX_MudarabahContracts_InvestorId",
            table: "MudarabahContracts",
            column: "InvestorId");
    }

    protected override void DownInitialCreate(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "ZakaatCalculations");
        migrationBuilder.DropTable(name: "MudarabahContracts");
    }
}