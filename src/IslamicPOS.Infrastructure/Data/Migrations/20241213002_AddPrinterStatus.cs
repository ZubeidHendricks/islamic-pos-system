using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations
{
    public partial class AddPrinterStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "PrinterConfigurations",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "LastStatus",
                table: "PrinterConfigurations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime?>(
                name: "LastStatusCheck",
                table: "PrinterConfigurations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrintErrorCount",
                table: "PrinterConfigurations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PrinterStatusLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrinterConfigurationId = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrinterStatusLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrinterStatusLogs_PrinterConfigurations_PrinterConfigurationId",
                        column: x => x.PrinterConfigurationId,
                        principalTable: "PrinterConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrinterStatusLogs_PrinterConfigurationId",
                table: "PrinterStatusLogs",
                column: "PrinterConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PrinterStatusLogs_Timestamp",
                table: "PrinterStatusLogs",
                column: "Timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrinterStatusLogs");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "PrinterConfigurations");

            migrationBuilder.DropColumn(
                name: "LastStatus",
                table: "PrinterConfigurations");

            migrationBuilder.DropColumn(
                name: "LastStatusCheck",
                table: "PrinterConfigurations");

            migrationBuilder.DropColumn(
                name: "PrintErrorCount",
                table: "PrinterConfigurations");
        }
    }
}