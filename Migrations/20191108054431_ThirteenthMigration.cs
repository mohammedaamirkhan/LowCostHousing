using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class ThirteenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LotNo",
                table: "ClientRegistration",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClientRegistration_ProjectMasterId",
                table: "ClientRegistration",
                column: "ProjectMasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "IX_ClientRegistration_ProjectMasterId",
                table: "ClientRegistration");

            migrationBuilder.DropColumn(
                name: "LotNo",
                table: "ClientRegistration");
        }
    }
}
