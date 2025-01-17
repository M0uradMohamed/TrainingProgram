using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_couse_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "BeginningDate",
                table: "Courses",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndingDate",
                table: "Courses",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImplementationMonth",
                table: "Courses",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginningDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EndingDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ImplementationMonth",
                table: "Courses");
        }
    }
}
