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
        public bool Status { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly HiringDate { get; set; }
        public string? MajorDegree { get; set; }
        public DateOnly MajorDegreeDate { get; set; }
        public string? Email { get; set; }
        public string? OtherPhoneNumber { get; set; }
    }
}
