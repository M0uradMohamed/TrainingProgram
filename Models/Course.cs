using Models.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? TargetSector { get; set; }
        public string? Participants { get; set; }
        public string? ImplementationPlace { get; set; }
        public int? DaysCount { get; set; }
        public string? ImplementedDays { get; set; }
        public DateOnly? BeginningDate { get; set; }
        public DateOnly? EndingDate { get; set; }
        public int? TraineesNumber { get; set; }
        public double? Cost { get; set; }
        public string? ImplementedCenter { get; set; }
        public int? HoursNumber { get; set; }
        public int? ImplementationTypeId { get; set; }
        public int? TotalImplementationId { get; set; }
        public int? RoomNumber { get; set; }
        public Material? Material { get; set; }
        public CourseType? CourseType { get; set; }
        public double? Rating { get; set; }
        public ImplementationMonth? ImplementationMonth { get; set; }
        public double? ActualCost { get; set; }
        public string? Code { get; set; }
        public Check? Check { get; set; }
        public string? PdfFile { get; set; }
        public string? EnterName { get; set; }
        public string? Link { get; set; }
        public string? RatingSpecialist { get; set; }
        public string? Notes { get; set; }
        public string? RatingSpecialistNotse { get; set; }
        public string? TraineesNotes { get; set; }

     //   public int? PrimaryInstructorId { get; set; }
        public int? CourseNatureId { get; set; }
        public int? TrainingSpecialistId { get; set; }

        public ICollection<Instructor>? Instructors { get; set; } = new List<Instructor>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
        public ICollection<CourseInstructor> CoursesInstructors { get; set; } = new List<CourseInstructor>();
        public ImplementationType? ImplementationType { get; set; }
        public TotalImplementation? TotalImplementation { get; set; }
        public CourseNature? CourseNature { get; set; }
    }
}
