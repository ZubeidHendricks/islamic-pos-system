using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinanceOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NisabThresholdAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NisabThresholdCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ZakaatRate = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    DefaultProfitSharingRatio = table.Column<decimal>(type: "decimal(5,4)", precision: 5, scale: 4, nullable: false),
                    FinancingTermInMonths = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZakaatCalculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalWealthAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalWealthCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    NisabThresholdAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NisabThresholdCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ZakaatAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ZakaatCurrency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    IsZakaatDue = table.Column<bool>(type: "bit", nullable: false),
                    CalculationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZakaatCalculations", x => x.Id);
                });

            // Seed initial finance options
            migrationBuilder.InsertData(
                table: "FinanceOptions",
                columns: new[] { "NisabThresholdAmount", "NisabThresholdCurrency", "ZakaatRate", 
                                "DefaultProfitSharingRatio", "FinancingTermInMonths", "Created", 
                                "CreatedBy", "LastModifiedBy" },
                values: new object[] { 5000.00m, "USD", 0.025m, 0.7m, 12, DateTime.UtcNow, "SYSTEM", "SYSTEM" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ZakaatCalculations");
            migrationBuilder.DropTable(name: "FinanceOptions");
        }
    }
}