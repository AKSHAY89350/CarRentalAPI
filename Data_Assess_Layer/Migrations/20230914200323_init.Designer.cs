﻿// <auto-generated />
using System;
using Data_Assess_Layer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data_Assess_Layer.Migrations
{
    [DbContext(typeof(CarRentalDbContext))]
    [Migration("20230914200323_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Data_Assess_Layer.Models.AdminLogin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("AdminLogins");
                });

            modelBuilder.Entity("Data_Assess_Layer.Models.CarDetails", b =>
                {
                    b.Property<int>("Vehicle_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Vehicle_Id"), 1L, 1);

                    b.Property<string>("Availability_status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image_Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maker")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double?>("Rental_Price")
                        .IsRequired()
                        .HasColumnType("float");

                    b.HasKey("Vehicle_Id");

                    b.ToTable("CarDetails");
                });

            modelBuilder.Entity("Data_Assess_Layer.Models.RentalAgreement", b =>
                {
                    b.Property<int>("RentalAgreementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RentalAgreementId"), 1L, 1);

                    b.Property<string>("IsReturned")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RentalDuration")
                        .HasColumnType("int");

                    b.Property<string>("RequestForReturn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Vehicle_Id")
                        .HasColumnType("int");

                    b.HasKey("RentalAgreementId");

                    b.HasIndex("UserId");

                    b.HasIndex("Vehicle_Id");

                    b.ToTable("RentalAgreement");
                });

            modelBuilder.Entity("Data_Assess_Layer.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Data_Assess_Layer.Models.RentalAgreement", b =>
                {
                    b.HasOne("Data_Assess_Layer.Models.User", "User")
                        .WithMany("RentalAgreements")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data_Assess_Layer.Models.CarDetails", "CarDetails")
                        .WithMany("RentalAgreements")
                        .HasForeignKey("Vehicle_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CarDetails");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data_Assess_Layer.Models.CarDetails", b =>
                {
                    b.Navigation("RentalAgreements");
                });

            modelBuilder.Entity("Data_Assess_Layer.Models.User", b =>
                {
                    b.Navigation("RentalAgreements");
                });
#pragma warning restore 612, 618
        }
    }
}
