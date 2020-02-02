using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IntegrationService.Migrations
{
    public partial class UpackageStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UpackageStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    StatusId = table.Column<long>(nullable: false),
                    UpackageId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpackageStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpackageStatuses_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpackageStatuses_Upackages_UpackageId",
                        column: x => x.UpackageId,
                        principalTable: "Upackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UpackageStatuses_StatusId",
                table: "UpackageStatuses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UpackageStatuses_UpackageId",
                table: "UpackageStatuses",
                column: "UpackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UpackageStatuses");
        }
    }
}
