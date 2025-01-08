using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? TraineeId { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Job { get; set; }
        public string? Department { get; set; }
        public string? Major { get; set; }
        public string? WorkPlace { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Belong  { get; set; }
        public string? CompanyNameForForeign  { get; set; }


        public int? SectorId { get; set; }
        public int? DegreeId { get; set; }

    }
}
