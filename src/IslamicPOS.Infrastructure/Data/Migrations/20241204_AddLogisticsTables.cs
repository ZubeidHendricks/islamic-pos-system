using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IslamicPOS.Infrastructure.Data.Migrations
{
    public partial class AddLogisticsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptimizedRoutes",
                columns: table => new
                {
                    RouteId = table.Column<Guid>(nullable: false),
                    EstimatedDuration = table.Column<TimeSpan>(nullable: false),
                    TotalDistance = table.Column<double>(nullable: false),
                    RecommendedVehicleType = table.Column<int>(nullable: false),
                    TotalWeight = table.Column<double>(nullable: false),
                    TotalVolume = table.Column<double>(nullable: false),
                    RequiresHalalSegregation = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptimizedRoutes", x => x.RouteId);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Volume = table.Column<double>(nullable: false),
                    RequiresHalalSegregation = table.Column<bool>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    OptimizedRouteId = table.Column<Guid>(nullable: true),
                    DeliveryWindow_Start = table.Column<DateTime>(nullable: true),
                    DeliveryWindow_End = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryPoints_OptimizedRoutes_OptimizedRouteId",
                        column: x => x.OptimizedRouteId,
                        principalTable: "OptimizedRoutes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPoints_OptimizedRouteId",
                table: "DeliveryPoints",
                column: "OptimizedRouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "DeliveryPoints");
            migrationBuilder.DropTable(name: "OptimizedRoutes");
        }
    }
}