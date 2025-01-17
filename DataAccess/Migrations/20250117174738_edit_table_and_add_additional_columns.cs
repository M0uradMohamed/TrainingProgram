using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class edit_table_and_add_additional_columns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginingDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EndingingDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ImplementationMonth",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ImplementationType",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "TestMark",
                table: "Trainees",
                newName: "WrittenExam");

            migrationBuilder.RenameColumn(
                name: "AttendanceDays",
                table: "Trainees",
                newName: "TotalEvaluation");

            migrationBuilder.RenameColumn(
                name: "TotalImplementation",
                table: "Courses",
                newName: "TotalImplementationId");

            migrationBuilder.AlterColumn<double>(
                name: "TotalMarks",
                table: "Trainees",
                type: "float",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Estimate",
                table: "Trainees",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AbsenceDays",
                table: "Trainees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ActivitiesMark",
                table: "Trainees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdherenceMark",
                table: "Trainees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttendanceAndDeparture",
                table: "Trainees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InteractionMark",
                table: "Trainees",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "courseType",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Material",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Check",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImplementationTypeId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ImplementationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImplementationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TotalImplementations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalImplementations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ImplementationTypeId",
                table: "Courses",
                column: "ImplementationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TotalImplementationId",
                table: "Courses",
                column: "TotalImplementationId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_ImplementationTypes_ImplementationTypeId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_TotalImplementations_TotalImplementationId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "ImplementationTypes");

            migrationBuilder.DropTable(
                name: "TotalImplementations");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ImplementationTypeId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TotalImplementationId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AbsenceDays",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "ActivitiesMark",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "AdherenceMark",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "AttendanceAndDeparture",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "InteractionMark",
                table: "Trainees");

            migrationBuilder.DropColumn(
                name: "ImplementationTypeId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "WrittenExam",
                table: "Trainees",
                newName: "TestMark");

            migrationBuilder.RenameColumn(
                name: "TotalEvaluation",
                table: "Trainees",
                newName: "AttendanceDays");

            migrationBuilder.RenameColumn(
                name: "TotalImplementationId",
                table: "Courses",
                newName: "TotalImplementation");

            migrationBuilder.AlterColumn<string>(
                name: "TotalMarks",
                table: "Trainees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estimate",
                table: "Trainees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "courseType",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Material",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Check",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "BeginingDate",
                table: "Courses",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndingingDate",
                table: "Courses",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ImplementationMonth",
                table: "Courses",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImplementationType",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }
    }
}
