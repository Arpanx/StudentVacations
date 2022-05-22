﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentVacations.Models;

#nullable disable

namespace StudentVacations.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220513143108_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StudentVacations.Models.Student", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentVacations.Models.Vacation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<int>("WeekNumberEnd")
                        .HasColumnType("int");

                    b.Property<int>("WeekNumberStart")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Vacations");
                });

            modelBuilder.Entity("StudentVacations.Models.Сourse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("StudentId")
                        .HasColumnType("bigint");

                    b.Property<int>("WeekNumberEnd")
                        .HasColumnType("int");

                    b.Property<int>("WeekNumberStart")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Сourses");
                });

            modelBuilder.Entity("StudentVacations.Models.Vacation", b =>
                {
                    b.HasOne("StudentVacations.Models.Student", "Student")
                        .WithMany("Vacations")
                        .HasForeignKey("StudentId");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentVacations.Models.Сourse", b =>
                {
                    b.HasOne("StudentVacations.Models.Student", "Student")
                        .WithMany("Сourses")
                        .HasForeignKey("StudentId");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentVacations.Models.Student", b =>
                {
                    b.Navigation("Vacations");

                    b.Navigation("Сourses");
                });
#pragma warning restore 612, 618
        }
    }
}