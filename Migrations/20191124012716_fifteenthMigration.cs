using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class fifteenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_ProjectMaster_ProjectMasterId",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectMasterId",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_ProjectMaster_ProjectMasterId",
                table: "Payments",
                column: "ProjectMasterId",
                principalTable: "ProjectMaster",
                principalColumn: "ProjectMasterId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_ProjectMaster_ProjectMasterId",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectMasterId",
                table: "Payments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_ProjectMaster_ProjectMasterId",
                table: "Payments",
                column: "ProjectMasterId",
                principalTable: "ProjectMaster",
                principalColumn: "ProjectMasterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
