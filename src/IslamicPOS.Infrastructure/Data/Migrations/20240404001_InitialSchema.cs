using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations;

public partial class InitialSchema : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Products Table
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                Barcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                StockQuantity = table.Column<int>(type: "int", nullable: false),
                Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                IsZakaatable = table.Column<bool>(type: "bit", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });

        // Stock Movements Table
        migrationBuilder.CreateTable(
            name: "StockMovements",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Reference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StockMovements", x => x.Id);
                table.ForeignKey(
                    name: "FK_StockMovements_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        // Transactions Table
        migrationBuilder.CreateTable(
            name: "Transactions",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transactions", x => x.Id);
            });

        // Transaction Items Table
        migrationBuilder.CreateTable(
            name: "TransactionItems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TransactionItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_TransactionItems_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_TransactionItems_Transactions_TransactionId",
                    column: x => x.TransactionId,
                    principalTable: "Transactions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        // Create Indexes
        migrationBuilder.CreateIndex(
            name: "IX_Products_Barcode",
            table: "Products",
            column: "Barcode",
            unique: true,
            filter: "[Barcode] IS NOT NULL");

        migrationBuilder.CreateIndex(
            name: "IX_StockMovements_ProductId",
            table: "StockMovements",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_TransactionItems_ProductId",
            table: "TransactionItems",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_TransactionItems_TransactionId",
            table: "TransactionItems",
            column: "TransactionId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "TransactionItems");
        migrationBuilder.DropTable(name: "Transactions");
        migrationBuilder.DropTable(name: "StockMovements");
        migrationBuilder.DropTable(name: "Products");
    }
}