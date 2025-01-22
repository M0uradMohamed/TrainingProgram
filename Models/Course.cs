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
        //edit , table #
        public int? ImplementationTypeId { get; set; }
        //edit, table #
        public int? TotalImplementationId { get; set; }
        public int? RoomNumber { get; set; }
        //enum #
        public Material? Material { get; set; }
        //enum #
        public courseType? courseType { get; set; }
        public double? Rating { get; set; }
        //edit , enum #
        public ImplementationMonth? ImplementationMonth { get; set; }
        public double? ActualCost { get; set; }
        public string? Code { get; set; }
        //enum #
        public Check? Check { get; set; }
        public string? PdfFile { get; set; }
        public string? Notes { get; set; }
        //user.name
        public string? EnterName { get; set; }

        public int? PrimaryInstructorId { get; set; }
        public int? CourseNatureId { get; set; }
        public int? TrainingSpecialistId { get; set; }


        public Instructor? PrimaryInstructor { get; set; }
        public ICollection<Instructor>? Instructors { get; set; } = new List<Instructor>();
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
        public ICollection<CourseInstructor> CoursesInstructors { get; set; } = new List<CourseInstructor>();
        public ImplementationType? implementationType { get; set; }
        public TotalImplementation? TotalImplementation { get; set; }
        public CourseNature? CourseNature { get; set; }
    }
}
