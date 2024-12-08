using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Users table
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<string>(nullable: false),
                UserName = table.Column<string>(maxLength: 256, nullable: false),
                NormalizedUserName = table.Column<string>(maxLength: 256, nullable: false),
                Email = table.Column<string>(maxLength: 256, nullable: false),
                NormalizedEmail = table.Column<string>(maxLength: 256, nullable: false),
                PasswordHash = table.Column<string>(nullable: false),
                SecurityStamp = table.Column<string>(nullable: false),
                FirstName = table.Column<string>(maxLength: 100, nullable: false),
                LastName = table.Column<string>(maxLength: 100, nullable: false),
                Role = table.Column<string>(maxLength: 50, nullable: false),
                IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                CreatedAt = table.Column<DateTime>(nullable: false),
                LastLogin = table.Column<DateTime>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        // Partners table
        migrationBuilder.CreateTable(
            name: "Partners",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(maxLength: 100, nullable: false),
                Email = table.Column<string>(maxLength: 256, nullable: true),
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

        // Add more tables...

        // Create indexes
        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            table: "Users",
            column: "NormalizedEmail",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Users_UserName",
            table: "Users",
            column: "NormalizedUserName",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Users");
        migrationBuilder.DropTable(name: "Partners");
        // Drop more tables...
    }
}