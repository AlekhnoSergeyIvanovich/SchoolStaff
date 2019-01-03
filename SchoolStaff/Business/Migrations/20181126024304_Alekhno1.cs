using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Alekhno1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameProfession = table.Column<string>(nullable: false),
                    CountPeopleProfession = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                    table.UniqueConstraint("AK_Professions_NameProfession", x => x.NameProfession);
                });

            migrationBuilder.CreateTable(
                name: "SchoolStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Patronymic = table.Column<string>(maxLength: 25, nullable: false),
                    Surname = table.Column<string>(maxLength: 30, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolStaffs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.UniqueConstraint("AK_Subjects_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "SchoolStaffPhones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Phone = table.Column<string>(maxLength: 15, nullable: true),
                    SchoolStaffId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolStaffPhones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolStaffPhones_SchoolStaffs_SchoolStaffId",
                        column: x => x.SchoolStaffId,
                        principalTable: "SchoolStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolStaffProfessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolStaffId = table.Column<int>(nullable: false),
                    ProfessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolStaffProfessions", x => x.Id);
                    table.UniqueConstraint("AK_SchoolStaffProfessions_SchoolStaffId_ProfessionId", x => new { x.SchoolStaffId, x.ProfessionId });
                    table.ForeignKey(
                        name: "FK_SchoolStaffProfessions_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolStaffProfessions_SchoolStaffs_SchoolStaffId",
                        column: x => x.SchoolStaffId,
                        principalTable: "SchoolStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchoolStaffSubjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchoolStaffId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolStaffSubjects", x => x.Id);
                    table.UniqueConstraint("AK_SchoolStaffSubjects_SchoolStaffId_SubjectId", x => new { x.SchoolStaffId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_SchoolStaffSubjects_SchoolStaffs_SchoolStaffId",
                        column: x => x.SchoolStaffId,
                        principalTable: "SchoolStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchoolStaffSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchoolStaffPhones_SchoolStaffId",
                table: "SchoolStaffPhones",
                column: "SchoolStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolStaffProfessions_ProfessionId",
                table: "SchoolStaffProfessions",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolStaffSubjects_SubjectId",
                table: "SchoolStaffSubjects",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchoolStaffPhones");

            migrationBuilder.DropTable(
                name: "SchoolStaffProfessions");

            migrationBuilder.DropTable(
                name: "SchoolStaffSubjects");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "SchoolStaffs");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
