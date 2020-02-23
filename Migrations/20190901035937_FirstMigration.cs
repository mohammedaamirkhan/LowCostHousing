using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientRegistration",
                columns: table => new
                {
                    ClientRegistrationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TypeOfOwnership = table.Column<bool>(nullable: false),
                    ClientGivenName = table.Column<string>(nullable: false),
                    ClientFamilyName = table.Column<string>(nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PassportNumber = table.Column<string>(nullable: true),
                    DrivingLicenseNumber = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: false),
                    HomePhoneNumber = table.Column<int>(nullable: false),
                    MobileNumber = table.Column<int>(nullable: false),
                    StreetNumber = table.Column<string>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    Suburb = table.Column<int>(nullable: false),
                    PostCode = table.Column<int>(nullable: false),
                    EmploymentType = table.Column<string>(nullable: true),
                    EmployerName = table.Column<string>(nullable: true),
                    PositionTitle = table.Column<string>(nullable: true),
                    NomineeGivenName = table.Column<string>(nullable: true),
                    NomineeFamilyName = table.Column<string>(nullable: true),
                    NomineePassportNumber = table.Column<string>(nullable: true),
                    NomineeMobileNumber = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRegistration", x => x.ClientRegistrationId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMaster",
                columns: table => new
                {
                    ProjectMasterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UnitNo = table.Column<int>(nullable: false),
                    ProjectName = table.Column<string>(nullable: false),
                    ReferenceNumber = table.Column<int>(nullable: false),
                    ProjectAddress = table.Column<string>(nullable: false),
                    AccountName = table.Column<string>(nullable: false),
                    AccountNumber = table.Column<int>(nullable: false),
                    BSB = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMaster", x => x.ProjectMasterId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientRegistration");

            migrationBuilder.DropTable(
                name: "ProjectMaster");
        }
    }
}
