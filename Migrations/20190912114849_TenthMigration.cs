using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class TenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Suburb",
                table: "ClientRegistration",
                newName: "SuburbID");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "ClientRegistration",
                newName: "StateID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuburbID",
                table: "ClientRegistration",
                newName: "Suburb");

            migrationBuilder.RenameColumn(
                name: "StateID",
                table: "ClientRegistration",
                newName: "State");
        }
    }
}
