using Models.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FoundationId { get; set; }
        public string? Name { get; set; }
        public Gender? Gender { get; set; }
        public string? Major { get; set; }
        public string? Job { get; set; }
        public string? Department { get; set; }
        public int? SectorId { get; set; }
        public int? DegreeId { get; set; }
        public string? WorkPlace { get; set; }
        public string? PhoneNumber { get; set; }
        public Belong? Belong  { get; set; }
        public string? CompanyNameForForeign  { get; set; }

      //  public Instructor? instructor { get; set; }
        public Sector? Sector { get; set; }
        public Degree? Degree { get; set; }
        public ICollection<Course>  Courses { get; set; } = new List<Course>();
        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();

    }

 
}
