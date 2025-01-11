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
        public int DaysCount { get; set; }
        public string? ImplementedDays { get; set; }
        public DateOnly? BeginingDate { get; set; }
        public DateOnly? EndingingDate { get; set; }
        public int TraineesNumber { get; set; }
        public double Cost { get; set; }
        public string? ImplementedCenter { get; set; }
        public int HoursNumber { get; set; }
        public string? ImplementationType { get; set; }
        //enum
        public string? TotalImplementation { get; set; }
        public int RoomNumber { get; set; }
        //enum
        public string? Material { get; set; }
        //enum
        public string? courseType { get; set; }
        public double Rating { get; set; }
        public DateOnly ImplementationMonth { get; set; }
        public double ActualCost { get; set; }
        public string? Code { get; set; }
        //enum
        public string? Check {  get; set; }
        public string? PdfFile { get; set; }
        public string? Notes {  get; set; }
        //user.name
        public string? EnterName { get; set; }

        public int? PrimaryInstructorId { get; set; }
        public int? CourseNatureId { get; set; }
        public int? TrainingSpecialistId { get; set; }
        public Instructor PrimaryInstructor { get; set; }
        public ICollection<Instructor> Instructors { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
