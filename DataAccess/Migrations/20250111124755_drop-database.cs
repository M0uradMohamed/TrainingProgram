using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class dropdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foreignes_Instructors_InstructorId_InstructorStatus",
                table: "Foreignes");

            migrationBuilder.DropTable(
                name: "CoursesInstructors");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "CourseNatures");

            migrationBuilder.DropTable(
                name: "TrainingSpecialists");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Foreignes");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "Sectors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    Belong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNameForForeign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeId = table.Column<int>(type: "int", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectorId = table.Column<int>(type: "int", nullable: true),
                    TraineeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkPlace = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimaryInstructorId = table.Column<int>(type: "int", nullable: true),
                    ActualCost = table.Column<double>(type: "float", nullable: false),
                    BeginingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Check = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: false),
                    CourseNatureId = table.Column<int>(type: "int", nullable: true),
                    DaysCount = table.Column<int>(type: "int", nullable: false),
                    EndingingDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EnterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoursNumber = table.Column<int>(type: "int", nullable: false),
                    ImplementationMonth = table.Column<DateOnly>(type: "date", nullable: false),
                    ImplementationPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementedCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementedDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Participants = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PdfFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    TargetSector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalImplementation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraineesNumber = table.Column<int>(type: "int", nullable: false),
                    TrainingSpecialistId = table.Column<int>(type: "int", nullable: true),
                    courseType = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                        name: "FK_Courses_TrainingSpecialists_TrainingSpecialistId",
                        column: x => x.TrainingSpecialistId,
                        principalTable: "TrainingSpecialists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Estimate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestMark = table.Column<double>(type: "float", nullable: false),
                    TotalMarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => new { x.CourseId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_Trainees_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursesInstructors",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    InstructorStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesInstructors", x => new { x.CourseId, x.InstructorId, x.InstructorStatus });
                    table.ForeignKey(
                        name: "FK_CoursesInstructors_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foreignes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstructorId = table.Column<int>(type: "int", nullable: true),
                    InstructorStatus = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foreignes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HiringDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MajorDegree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MajorDegreeDate = table.Column<DateOnly>(type: "date", nullable: false),
                    OtherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => new { x.Id, x.Status });
                    table.UniqueConstraint("AK_Instructors_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Employees_Id",
                        column: x => x.Id,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Instructors_Foreignes_Id",
                        column: x => x.Id,
                        principalTable: "Foreignes",
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
                name: "IX_CoursesInstructors_InstructorId_InstructorStatus",
                table: "CoursesInstructors",
                columns: new[] { "InstructorId", "InstructorStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DegreeId",
                table: "Employees",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SectorId",
                table: "Employees",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Foreignes_InstructorId_InstructorStatus",
                table: "Foreignes",
                columns: new[] { "InstructorId", "InstructorStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_EmployeeId",
                table: "Trainees",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Instructors_PrimaryInstructorId",
                table: "Courses",
                column: "PrimaryInstructorId",
                principalTable: "Instructors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesInstructors_Instructors_InstructorId_InstructorStatus",
                table: "CoursesInstructors",
                columns: new[] { "InstructorId", "InstructorStatus" },
                principalTable: "Instructors",
                principalColumns: new[] { "Id", "Status" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Foreignes_Instructors_InstructorId_InstructorStatus",
                table: "Foreignes",
                columns: new[] { "InstructorId", "InstructorStatus" },
                principalTable: "Instructors",
                principalColumns: new[] { "Id", "Status" });
        }
    }
}
