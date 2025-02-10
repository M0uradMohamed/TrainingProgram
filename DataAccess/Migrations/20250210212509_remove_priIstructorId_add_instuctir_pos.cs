using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class remove_priIstructorId_add_instuctir_pos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryInstructorId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "CoursesInstructors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "CoursesInstructors");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryInstructorId",
                table: "Courses",
                type: "int",
                nullable: true);
        }
    }
}
