using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LowCostHousing.Migrations
{
    public partial class spSavePayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"--Insert data into payment master--
                        Create Procedure SavePayments

                        @ClientRegistrationId int, 
                        @TotalAmount float,
                        @AmountPaid float,
                        @PaidOn varchar(MAX),
                        @ReceivedBy int,
                        @CreatedBy int,
                        @Description Varchar(MAX),
                        @FileName Varchar(MAX),
                        @FilePath Varchar(MAX)

                        AS
                        BEGIN
                        INSERT INTO Payments(ClientRegistrationId, TotalAmount,AmountPaid,PaidOn,ReceivedBy,CreatedOn,CreatedBy,
                                             Description,FileName,FilePath)
		                            Values(@ClientRegistrationId,@TotalAmount,@AmountPaid,@PaidOn,@ReceivedBy,GETDATE(),@CreatedBy,
			                               @Description,@FileName,@FilePath)


                        END";
            migrationBuilder.Sql(sp);   
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
