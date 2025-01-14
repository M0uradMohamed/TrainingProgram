using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class right_database_build : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseNatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseNatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSpecialists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSpecialists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoundationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorId = table.Column<int>(type: "int", nullable: true),
                    DegreeId = table.Column<int>(type: "int", nullable: true),
                    WorkPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Belong = table.Column<bool>(type: "bit", nullable: true),
                    CompanyNameForForeign = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoundationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HiringDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeId = table.Column<int>(type: "int", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorId = table.Column<int>(type: "int", nullable: true),
                    WorkPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduationeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicDegree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicDegreeeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Instructors_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Instructors_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TargetSector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Participants = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementationPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaysCount = table.Column<int>(type: "int", nullable: true),
                    ImplementedDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndingingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TraineesNumber = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    ImplementedCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoursNumber = table.Column<int>(type: "int", nullable: true),
                    ImplementationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalImplementation = table.Column<int>(type: "int", nullable: true),
                    RoomNumber = table.Column<int>(type: "int", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    courseType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    ImplementationMonth = table.Column<DateOnly>(type: "date", nullable: true),
                    ActualCost = table.Column<double>(type: "float", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Check = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PdfFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    EnterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryInstructorId = table.Column<int>(type: "int", nullable: true),
                    CourseNatureId = table.Column<int>(type: "int", nullable: true),
                    TrainingSpecialistId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_CourseNatures_CourseNatureId",
                        column: x => x.CourseNatureId,
                        principalTable: "CourseNatures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_Instructors_PrimaryInstructorId",
                        column: x => x.PrimaryInstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Courses_TrainingSpecialists_TrainingSpecialistId",
                        column: x => x.TrainingSpecialistId,
                        principalTable: "TrainingSpecialists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoursesInstructors",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesInstructors", x => new { x.CourseId, x.InstructorId });
                    table.ForeignKey(
                        name: "FK_CoursesInstructors_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CoursesInstructors_Instructors_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "Instructors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Estimate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestMark = table.Column<double>(type: "float", nullable: true),
                    TotalMarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => new { x.EmployeeId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Trainees_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trainees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseNatureId",
                table: "Courses",
                column: "CourseNatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PrimaryInstructorId",
                table: "Courses",
                column: "PrimaryInstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TrainingSpecialistId",
                table: "Courses",
                column: "TrainingSpecialistId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesInstructors_InstructorId",
                table: "CoursesInstructors",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DegreeId",
                table: "Employees",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SectorId",
                table: "Employees",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DegreeId",
                table: "Instructors",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_EmployeeId",
                table: "Instructors",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_SectorId",
                table: "Instructors",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_CourseId",
                table: "Trainees",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesInstructors");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "CourseNatures");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "TrainingSpecialists");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Sectors");
        }
    }
}
