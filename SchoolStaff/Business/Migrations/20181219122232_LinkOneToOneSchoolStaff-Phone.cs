using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class LinkOneToOneSchoolStaffPhone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrimaryPhoneId",
                table: "SchoolStaffs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolStaffs_PrimaryPhoneId",
                table: "SchoolStaffs",
                column: "PrimaryPhoneId",
                unique: true,
                filter: "[PrimaryPhoneId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolStaffs_SchoolStaffPhones_PrimaryPhoneId",
                table: "SchoolStaffs",
                column: "PrimaryPhoneId",
                principalTable: "SchoolStaffPhones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolStaffs_SchoolStaffPhones_PrimaryPhoneId",
                table: "SchoolStaffs");

            migrationBuilder.DropIndex(
                name: "IX_SchoolStaffs_PrimaryPhoneId",
                table: "SchoolStaffs");

            migrationBuilder.DropColumn(
                name: "PrimaryPhoneId",
                table: "SchoolStaffs");
        }
    }
}
