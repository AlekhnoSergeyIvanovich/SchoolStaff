using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class DefaultTimeOfMassege : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeWrite",
                table: "SentMessages");

            migrationBuilder.DropColumn(
                name: "sentEmailOrSMS",
                table: "SentMessages");

            migrationBuilder.DropColumn(
                name: "sentEmailOrSMS",
                table: "SchoolStaffs");

            migrationBuilder.AddColumn<int>(
                name: "TypeMessage",
                table: "SentMessages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "WriteTime",
                table: "SentMessages",
                nullable: false,
                defaultValueSql: "GETUTCDATE()"); //new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "typeMessage",
                table: "SchoolStaffs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeMessage",
                table: "SentMessages");

            migrationBuilder.DropColumn(
                name: "WriteTime",
                table: "SentMessages");

            migrationBuilder.DropColumn(
                name: "typeMessage",
                table: "SchoolStaffs");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeWrite",
                table: "SentMessages",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 11, 23, 6, 55, 263, DateTimeKind.Local));

            migrationBuilder.AddColumn<int>(
                name: "sentEmailOrSMS",
                table: "SentMessages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sentEmailOrSMS",
                table: "SchoolStaffs",
                nullable: false,
                defaultValue: 0);
        }
    }
}
