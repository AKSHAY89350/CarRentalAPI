using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Assess_Layer.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentalDuration",
                table: "RentalAgreement");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "RentalAgreement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "RentalAgreement",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "RentalAgreement");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "RentalAgreement");

            migrationBuilder.AddColumn<int>(
                name: "RentalDuration",
                table: "RentalAgreement",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
