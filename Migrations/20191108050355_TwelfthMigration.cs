using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class TwelfthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientRegistrationId",
                table: "Payments",
                column: "ClientRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ProjectMasterId",
                table: "Payments",
                column: "ProjectMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_ClientRegistration_ClientRegistrationId",
                table: "Payments",
                column: "ClientRegistrationId",
                principalTable: "ClientRegistration",
                principalColumn: "ClientRegistrationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_ProjectMaster_ProjectMasterId",
                table: "Payments",
                column: "ProjectMasterId",
                principalTable: "ProjectMaster",
                principalColumn: "ProjectMasterId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_ClientRegistration_ClientRegistrationId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_ProjectMaster_ProjectMasterId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ClientRegistrationId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ProjectMasterId",
                table: "Payments");
        }
    }
}
