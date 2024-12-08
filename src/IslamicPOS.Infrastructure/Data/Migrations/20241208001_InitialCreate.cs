using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Partners",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(maxLength: 100, nullable: false),
                SharePercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                InvestmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                JoinDate = table.Column<DateTime>(nullable: false),
                LastDistributionDate = table.Column<DateTime>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Partners", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(maxLength: 100, nullable: false),
                Description = table.Column<string>(maxLength: 500, nullable: true),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                StockQuantity = table.Column<int>(nullable: false),
                Barcode = table.Column<string>(maxLength: 50, nullable: true),
                IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                CreatedAt = table.Column<DateTime>(nullable: false),
                UpdatedAt = table.Column<DateTime>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ProfitDistributions",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PartnerId = table.Column<int>(nullable: false),
                Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                DistributionDate = table.Column<DateTime>(nullable: false),
                Status = table.Column<string>(maxLength: 50, nullable: false),
                Reference = table.Column<string>(maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProfitDistributions", x => x.Id);
                table.ForeignKey(
                    name: "FK_ProfitDistributions_Partners_PartnerId",
                    column: x => x.PartnerId,
                    principalTable: "Partners",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        // Add indexes
        migrationBuilder.CreateIndex(
            name: "IX_Products_Barcode",
            table: "Products",
            column: "Barcode",
            unique: true,
            filter: "[Barcode] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_ProfitDistributions_PartnerId",
            table: "ProfitDistributions",
            column: "PartnerId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "ProfitDistributions");
        migrationBuilder.DropTable(name: "Products");
        migrationBuilder.DropTable(name: "Partners");
    }
}