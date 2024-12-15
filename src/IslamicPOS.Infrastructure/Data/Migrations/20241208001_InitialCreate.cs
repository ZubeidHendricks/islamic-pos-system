using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        UpInitialCreate(migrationBuilder);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        DownInitialCreate(migrationBuilder);
    }

    partial void UpInitialCreate(MigrationBuilder migrationBuilder);
    partial void DownInitialCreate(MigrationBuilder migrationBuilder);
}