using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class NinthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuburnName",
                table: "Suburb",
                newName: "SuburbName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SuburbName",
                table: "Suburb",
                newName: "SuburnName");
        }
    }
}
