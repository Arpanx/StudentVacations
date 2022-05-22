using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentVacations.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Сourses");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "Сourses");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "Vacations");

            migrationBuilder.AddColumn<int>(
                name: "WeekNumberEnd",
                table: "Сourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeekNumberStart",
                table: "Сourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeekNumberEnd",
                table: "Vacations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeekNumberStart",
                table: "Vacations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WeekNumberEnd",
                table: "Сourses");

            migrationBuilder.DropColumn(
                name: "WeekNumberStart",
                table: "Сourses");

            migrationBuilder.DropColumn(
                name: "WeekNumberEnd",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "WeekNumberStart",
                table: "Vacations");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "Сourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "Сourses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "Vacations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "Vacations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
