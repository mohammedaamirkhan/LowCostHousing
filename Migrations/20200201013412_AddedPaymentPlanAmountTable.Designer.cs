﻿// <auto-generated />
using System;
using LowCostHousing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LowCostHousing.Migrations
{
    [DbContext(typeof(LowCostHousingDBcontext))]
    [Migration("20200201013412_AddedPaymentPlanAmountTable")]
    partial class AddedPaymentPlanAmountTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LowCostHousing.Models.ClientRegistration", b =>
                {
                    b.Property<int>("ClientRegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientFamilyName")
                        .IsRequired();

                    b.Property<string>("ClientGivenName")
                        .IsRequired();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("DrivingLicenseNumber");

                    b.Property<string>("EmailAddress")
                        .IsRequired();

                    b.Property<string>("EmployerName");

                    b.Property<string>("EmploymentType");

                    b.Property<string>("Gender");

                    b.Property<int>("HomePhoneNumber");

                    b.Property<int>("LotNo");

                    b.Property<int>("MobileNumber");

                    b.Property<string>("NomineeFamilyName");

                    b.Property<string>("NomineeGivenName");

                    b.Property<string>("NomineeMobileNumber");

                    b.Property<string>("NomineePassportNumber");

                    b.Property<string>("PassportNumber");

                    b.Property<string>("PositionTitle");

                    b.Property<int>("PostCode");

                    b.Property<int>("ProjectMasterId");

                    b.Property<int>("ReferenceNumber");

                    b.Property<int>("StateID");

                    b.Property<string>("StreetName");

                    b.Property<string>("StreetNumber");

                    b.Property<int>("SuburbID");

                    b.Property<bool>("TypeOfOwnership");

                    b.HasKey("ClientRegistrationId");

                    b.HasIndex("ProjectMasterId");

                    b.ToTable("ClientRegistration");
                });

            modelBuilder.Entity("LowCostHousing.Models.PaymentPlan", b =>
                {
                    b.Property<int>("PaymentPlanId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientRegistrationId");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<double>("DevelopmentCost");

                    b.Property<double>("HoldingDeposit");

                    b.Property<double>("LandCost");

                    b.Property<int>("ProjectMasterId");

                    b.Property<double>("TotalCost");

                    b.HasKey("PaymentPlanId");

                    b.ToTable("PaymentPlan");
                });

            modelBuilder.Entity("LowCostHousing.Models.PaymentPlanAmount", b =>
                {
                    b.Property<int>("PaymentPlanAmountId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount");

                    b.Property<int>("ClientRegistrationId");

                    b.Property<int>("Dates");

                    b.HasKey("PaymentPlanAmountId");

                    b.ToTable("PaymentPlanAmount");
                });

            modelBuilder.Entity("LowCostHousing.Models.Payments", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AmountPaid");

                    b.Property<int>("ClientRegistrationId");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(2147483647);

                    b.Property<string>("FileName");

                    b.Property<string>("FilePath")
                        .HasMaxLength(2147483647);

                    b.Property<DateTime>("PaidOn");

                    b.Property<int?>("ProjectMasterId");

                    b.Property<int>("ReceivedBy");

                    b.Property<double>("TotalAmount");

                    b.HasKey("PaymentId");

                    b.HasIndex("ClientRegistrationId");

                    b.HasIndex("ProjectMasterId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("LowCostHousing.Models.ProjectMaster", b =>
                {
                    b.Property<int>("ProjectMasterId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired();

                    b.Property<int>("AccountNumber");

                    b.Property<int>("BSB");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("ProjectAddress")
                        .IsRequired();

                    b.Property<string>("ProjectName")
                        .IsRequired();

                    b.Property<int>("ReferenceNumber");

                    b.Property<int>("UnitNo");

                    b.HasKey("ProjectMasterId");

                    b.ToTable("ProjectMaster");
                });

            modelBuilder.Entity("LowCostHousing.Models.State", b =>
                {
                    b.Property<int>("StateID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StateName");

                    b.HasKey("StateID");

                    b.ToTable("State");
                });

            modelBuilder.Entity("LowCostHousing.Models.Suburb", b =>
                {
                    b.Property<int>("SuburbID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StateID");

                    b.Property<string>("SuburbName");

                    b.HasKey("SuburbID");

                    b.ToTable("Suburb");
                });

            modelBuilder.Entity("LowCostHousing.Models.ClientRegistration", b =>
                {
                    b.HasOne("LowCostHousing.Models.ProjectMaster", "ProjectMaster")
                        .WithMany()
                        .HasForeignKey("ProjectMasterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LowCostHousing.Models.Payments", b =>
                {
                    b.HasOne("LowCostHousing.Models.ClientRegistration", "ClientRegistration")
                        .WithMany()
                        .HasForeignKey("ClientRegistrationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LowCostHousing.Models.ProjectMaster", "ProjectMaster")
                        .WithMany()
                        .HasForeignKey("ProjectMasterId");
                });
#pragma warning restore 612, 618
        }
    }
}
