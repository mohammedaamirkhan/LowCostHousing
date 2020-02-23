using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "ClientRegistration",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "ClientRegistration",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
