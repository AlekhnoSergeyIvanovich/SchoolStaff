using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Alekhno6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "SentMessages");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeWrite",
                table: "SentMessages",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 11, 23, 6, 55, 263, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeWrite",
                table: "SentMessages");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "SentMessages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
