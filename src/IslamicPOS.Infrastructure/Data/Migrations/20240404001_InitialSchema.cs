using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations;

public partial class InitialSchema : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        UpInitialSchema(migrationBuilder);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        DownInitialSchema(migrationBuilder);
    }

    partial void UpInitialSchema(MigrationBuilder migrationBuilder);
    partial void DownInitialSchema(MigrationBuilder migrationBuilder);
}