using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_course_traineerating_coulmu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RatingSpecialistNotse",
                table: "Courses",
                newName: "RatingSpecialistNotes");

            migrationBuilder.AddColumn<double>(
                name: "TraineesRating",
                table: "Courses",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TraineesRating",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "RatingSpecialistNotes",
                table: "Courses",
                newName: "RatingSpecialistNotse");
        }
    }
}
