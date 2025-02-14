using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class modify_relations_course_Employee_instructor_with_theRestTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseNatures_CourseNatureId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ImplementationTypes_ImplementationTypeId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_TotalImplementations_TotalImplementationId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_TrainingSpecialists_TrainingSpecialistId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Degrees_DegreeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Sectors_SectorId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Degrees_DegreeId",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Sectors_SectorId",
                table: "Instructors");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseNatures_CourseNatureId",
                table: "Courses",
                column: "CourseNatureId",
                principalTable: "CourseNatures",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_ImplementationTypes_ImplementationTypeId",
                table: "Courses",
                column: "ImplementationTypeId",
                principalTable: "ImplementationTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_TotalImplementations_TotalImplementationId",
                table: "Courses",
                column: "TotalImplementationId",
                principalTable: "TotalImplementations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_TrainingSpecialists_TrainingSpecialistId",
                table: "Courses",
                column: "TrainingSpecialistId",
                principalTable: "TrainingSpecialists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Degrees_DegreeId",
                table: "Employees",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Sectors_SectorId",
                table: "Employees",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Degrees_DegreeId",
                table: "Instructors",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Sectors_SectorId",
                table: "Instructors",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseNatures_CourseNatureId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ImplementationTypes_ImplementationTypeId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_TotalImplementations_TotalImplementationId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_TrainingSpecialists_TrainingSpecialistId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Degrees_DegreeId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Sectors_SectorId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Degrees_DegreeId",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Sectors_SectorId",
                table: "Instructors");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseNatures_CourseNatureId",
                table: "Courses",
                column: "CourseNatureId",
                principalTable: "CourseNatures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_ImplementationTypes_ImplementationTypeId",
                table: "Courses",
                column: "ImplementationTypeId",
                principalTable: "ImplementationTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_TotalImplementations_TotalImplementationId",
                table: "Courses",
                column: "TotalImplementationId",
                principalTable: "TotalImplementations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_TrainingSpecialists_TrainingSpecialistId",
                table: "Courses",
                column: "TrainingSpecialistId",
                principalTable: "TrainingSpecialists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Degrees_DegreeId",
                table: "Employees",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Sectors_SectorId",
                table: "Employees",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Degrees_DegreeId",
                table: "Instructors",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Sectors_SectorId",
                table: "Instructors",
                column: "SectorId",
                principalTable: "Sectors",
                principalColumn: "Id");
        }
    }
}
