using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class SeveenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Payments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountPaid = table.Column<double>(nullable: false),
                    ClientRegistrationId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 2147483647, nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(maxLength: 2147483647, nullable: true),
                    PaidOn = table.Column<DateTime>(nullable: false),
                    ProjectMasterId = table.Column<int>(nullable: true),
                    ReceivedBy = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_ClientRegistration_ClientRegistrationId",
                        column: x => x.ClientRegistrationId,
                        principalTable: "ClientRegistration",
                        principalColumn: "ClientRegistrationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientRegistrationId",
                table: "Payments",
                column: "ClientRegistrationId");
        }
    }
}
