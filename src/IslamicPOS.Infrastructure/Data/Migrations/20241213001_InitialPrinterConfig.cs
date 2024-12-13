using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations
{
    public partial class InitialPrinterConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrinterConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrinterName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    PaperSize = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Copies = table.Column<int>(type: "int", nullable: false),
                    PrintLogo = table.Column<bool>(type: "bit", nullable: false),
                    PrintHalalCertification = table.Column<bool>(type: "bit", nullable: false),
                    HeaderText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FooterText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterConfigurations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrinterConfigurations_IsDefault",
                table: "PrinterConfigurations",
                column: "IsDefault");

            migrationBuilder.CreateIndex(
                name: "IX_PrinterConfigurations_PrinterName",
                table: "PrinterConfigurations",
                column: "PrinterName",
                unique: true);

            // Insert default configuration
            migrationBuilder.InsertData(
                table: "PrinterConfigurations",
                columns: new[] { "PrinterName", "IsDefault", "PaperSize", "Copies", 
                               "PrintLogo", "PrintHalalCertification", "HeaderText", 
                               "FooterText", "LastUpdated" },
                values: new object[] {
                    "Default Printer", true, "80mm", 1, true, true,
                    "Islamic POS System",
                    "Thank you for your business\nMay Allah bless your purchase",
                    DateTime.UtcNow
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrinterConfigurations");
        }
    }
}