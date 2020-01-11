using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteMap",
                columns: table => new
                {
                    IntegrationId = table.Column<string>(nullable: false),
                    SystemId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteMap", x => new { x.IntegrationId, x.SystemId });
                });

            migrationBuilder.CreateTable(
                name: "Upackages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntegrationId = table.Column<string>(nullable: false),
                    SystemId = table.Column<string>(nullable: false),
                    Data = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upackages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteMap");

            migrationBuilder.DropTable(
                name: "Upackages");
        }
    }
}
