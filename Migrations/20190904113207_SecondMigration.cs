using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectMasterId",
                table: "ClientRegistration",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectMasterId",
                table: "ClientRegistration");
        }
    }
}
