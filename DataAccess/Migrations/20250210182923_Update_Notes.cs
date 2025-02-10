using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update_Notes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Instructors_PrimaryInstructorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesInstructors_Courses_CourseId",
                table: "CoursesInstructors");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesInstructors_Instructors_InstructorId",
                table: "CoursesInstructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Courses_CourseId",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Employees_EmployeeId",
                table: "Trainees");

            migrationBuilder.DropIndex(
                name: "IX_Courses_PrimaryInstructorId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "courseType",
                table: "Courses",
                newName: "CourseType");

            migrationBuilder.AddColumn<string>(
                name: "SecondNotes",
                table: "Trainees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Certified",
                table: "Instructors",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseNotes",
                table: "CoursesInstructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "CoursesInstructors",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RatingSpecialist",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RatingSpecialistNotse",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TraineesNotes",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesInstructors_Courses_CourseId",
                table: "CoursesInstructors",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesInstructors_Instructors_InstructorId",
                table: "CoursesInstructors",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Courses_CourseId",
                table: "Trainees",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Employees_EmployeeId",
                table: "Trainees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesInstructors_Courses_CourseId",
                table: "CoursesInstructors");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursesInstructors_Instructors_InstructorId",
                table: "CoursesInstructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Courses_CourseId",
                table: "Trainees");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainees_Employees_EmployeeId",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "SecondNotes",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "Certified",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "CourseNotes",
                table: "CoursesInstructors");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "CoursesInstructors");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "RatingSpecialist",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "RatingSpecialistNotse",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TraineesNotes",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CourseType",
                table: "Courses",
                newName: "courseType");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PrimaryInstructorId",
                table: "Courses",
                column: "PrimaryInstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Instructors_PrimaryInstructorId",
                table: "Courses",
                column: "PrimaryInstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesInstructors_Courses_CourseId",
                table: "CoursesInstructors",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesInstructors_Instructors_InstructorId",
                table: "CoursesInstructors",
                column: "InstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Courses_CourseId",
                table: "Trainees",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainees_Employees_EmployeeId",
                table: "Trainees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
