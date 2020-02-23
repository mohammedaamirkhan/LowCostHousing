using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class spGetPaymentDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"Create PROCEDURE GetPaymentDetails 
                        @ClientRegistrationId int

                        AS
                        BEGIN
	                        SET NOCOUNT ON;

	                        Select DISTINCT CR.ClientGivenName,CR.ClientFamilyName,PM.ProjectName,CR.MobileNumber,CR.LotNo,CR.ReferenceNumber,CR.ClientRegistrationId,
	                        PM.ProjectMasterId
	                        from ClientRegistration CR
	                        inner join ProjectMaster PM on PM.ProjectMasterId=CR.ProjectMasterId
	                        where CR.ClientRegistrationId=@ClientRegistrationId and (CR.ProjectMasterId is not null)
                        END
                        ";
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
