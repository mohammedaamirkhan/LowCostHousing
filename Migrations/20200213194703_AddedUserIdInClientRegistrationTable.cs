using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class AddedUserIdInClientRegistrationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ClientRegistration",
                nullable: true);

            migrationBuilder.DropColumn(
               name: "ClientRegistrationId",
               table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClientRegistration");

            migrationBuilder.DropColumn(
                name: "ClientRegistrationId",
                table: "AspNetUsers");
        }
    }
}
