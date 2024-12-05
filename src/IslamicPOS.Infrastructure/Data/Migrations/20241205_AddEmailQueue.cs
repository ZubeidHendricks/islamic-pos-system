using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations
{
    public partial class AddEmailQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Subject = table.Column<string>(maxLength: 200, nullable: false),
                    HtmlBody = table.Column<string>(type: "text", nullable: false),
                    TextBody = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Language = table.Column<string>(maxLength: 10, nullable: false),
                    RequiredVariables = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailQueue",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    To = table.Column<string>(maxLength: 256, nullable: false),
                    Subject = table.Column<string>(maxLength: 200, nullable: false),
                    HtmlContent = table.Column<string>(type: "text", nullable: false),
                    TextContent = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(nullable: false),
                    RetryCount = table.Column<int>(nullable: false),
                    ErrorMessage = table.Column<string>(maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    SentAt = table.Column<DateTime>(nullable: true),
                    NextRetryAt = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: false),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailQueue", x => x.Id);
                });

            // Add default email templates
            var templatePath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, 
                "Email", 
                "Templates");

            var registrationTemplate = File.ReadAllText(
                Path.Combine(templatePath, "registration-validation.html"));
            var welcomeTemplate = File.ReadAllText(
                Path.Combine(templatePath, "welcome-email.html"));
            var invoiceTemplate = File.ReadAllText(
                Path.Combine(templatePath, "invoice-receipt.html"));

            migrationBuilder.InsertData(
                table: "EmailTemplates",
                columns: new[] { "Id", "Name", "Subject", "HtmlBody", "TextBody", "IsActive", "Language", "RequiredVariables" },
                values: new object[,]
                {
                    {
                        Guid.NewGuid(),
                        "registration-validation",
                        "Validate Your Islamic POS Registration",
                        registrationTemplate,
                        null,
                        true,
                        "en",
                        "[\"Name\",\"ValidationLink\",\"Domain\",\"ExpiryHours\"]"
                    },
                    {
                        Guid.NewGuid(),
                        "welcome-email",
                        "Welcome to Islamic POS",
                        welcomeTemplate,
                        null,
                        true,
                        "en",
                        "[\"Name\",\"LoginLink\",\"Domain\",\"SupportEmail\"]"
                    },
                    {
                        Guid.NewGuid(),
                        "invoice-receipt",
                        "Invoice Receipt - {{InvoiceNumber}}",
                        invoiceTemplate,
                        null,
                        true,
                        "en",
                        "[\"InvoiceNumber\",\"InvoiceDate\",\"CompanyName\",\"Currency\",\"Total\"]"
                    }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailQueue_Status_Priority_NextRetryAt",
                table: "EmailQueue",
                columns: new[] { "Status", "Priority", "NextRetryAt" });

            migrationBuilder.CreateIndex(
                name: "IX_EmailQueue_TenantId",
                table: "EmailQueue",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "EmailQueue");
            migrationBuilder.DropTable(name: "EmailTemplates");
        }
    }
}