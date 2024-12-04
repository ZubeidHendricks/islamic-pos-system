using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations
{
    public partial class AddLabelTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabelTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Size = table.Column<int>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Layout = table.Column<string>(type: "jsonb", nullable: true),
                    Styling = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelTemplates", x => x.Id);
                });

            // Add default template
            migrationBuilder.InsertData(
                table: "LabelTemplates",
                columns: new[] { "Id", "Name", "Description", "Size", "IsDefault", "Layout", "Styling" },
                values: new object[] {
                    "default",
                    "Standard Label",
                    "Default label template",
                    1, // Standard size
                    true,
                    "{\"logoWidth\": 100, \"logoHeight\": 100, \"barcodeWidth\": 200, \"barcodeHeight\": 100, \"titleHeight\": 20, \"contentHeight\": 20, \"margin\": 10, \"padding\": 5, \"rotateBarcode\": false}",
                    "{\"backgroundColor\": \"#FFFFFF\", \"textColor\": \"#000000\", \"fontFamily\": \"Arial\", \"borderWidth\": 0, \"roundedCorners\": false, \"cornerRadius\": 0}"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "LabelTemplates");
        }
    }
}