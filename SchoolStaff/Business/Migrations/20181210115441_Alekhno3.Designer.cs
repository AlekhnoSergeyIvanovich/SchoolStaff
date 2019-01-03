﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181210115441_Alekhno3")]
    partial class Alekhno3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Entities.Profession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CountPeopleProfession");

                    b.Property<string>("NameProfession")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("NameProfession");

                    b.ToTable("Professions");
                });

            modelBuilder.Entity("DAL.Entities.SchoolStaff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("sentEmailOrSMS");

                    b.HasKey("Id");

                    b.ToTable("SchoolStaffs");
                });

            modelBuilder.Entity("DAL.Entities.SchoolStaffPhone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Phone")
                        .HasMaxLength(15);

                    b.Property<int?>("SchoolStaffId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolStaffId");

                    b.ToTable("SchoolStaffPhones");
                });

            modelBuilder.Entity("DAL.Entities.SchoolStaffProfession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProfessionId")
                        .IsRequired();

                    b.Property<int?>("SchoolStaffId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SchoolStaffId", "ProfessionId");

                    b.HasIndex("ProfessionId");

                    b.ToTable("SchoolStaffProfessions");
                });

            modelBuilder.Entity("DAL.Entities.SchoolStaffSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("SchoolStaffId")
                        .IsRequired();

                    b.Property<int?>("SubjectId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("SchoolStaffId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SchoolStaffSubjects");
                });

            modelBuilder.Entity("DAL.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("DAL.Entities.SchoolStaffPhone", b =>
                {
                    b.HasOne("DAL.Entities.SchoolStaff", "SchoolStaff")
                        .WithMany("SchoolStaffPhones")
                        .HasForeignKey("SchoolStaffId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Entities.SchoolStaffProfession", b =>
                {
                    b.HasOne("DAL.Entities.Profession", "Profession")
                        .WithMany("SchoolStaffProfession")
                        .HasForeignKey("ProfessionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Entities.SchoolStaff", "SchoolStaff")
                        .WithMany("SchoolStaffProfession")
                        .HasForeignKey("SchoolStaffId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Entities.SchoolStaffSubject", b =>
                {
                    b.HasOne("DAL.Entities.SchoolStaff", "SchoolStaff")
                        .WithMany("SchoolStaffSubjects")
                        .HasForeignKey("SchoolStaffId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Entities.Subject", "Subject")
                        .WithMany("SchoolStaffSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
