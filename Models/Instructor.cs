using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string? FoundationId { get; set; }
        public string? Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? HiringDate { get; set; }
        public int? DegreeId { get; set; }
        public string? Department { get; set; }
        public int? SectorId { get; set; }
        public string? WorkPlace { get; set; }
        public string? Major { get; set; }
        public string? GraduationeDate { get; set; }
        public string? AcademicDegree { get; set; }
        public string? AcademicDegreeeDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? OtherPhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool? Certified { get; set; } 

     //   public int? EmployeeId { get; set; }

        public Degree? Degree { get; set; }
        public Sector? Sector { get; set; }
     //   public Employee? Employee { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();
     //   public ICollection<Course> PrimaryCourses { get; set; } = new List<Course>();
        public ICollection<CourseInstructor> CoursesInstructors { get; set; } = new List<CourseInstructor>();

    }
}
