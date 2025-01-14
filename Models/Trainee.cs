using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Trainee
    {
        public int CourseId { get; set; }
        public int EmployeeId { get; set; }
        //enum
        public string? Estimate {  get; set; }
        public double? TestMark {  get; set; }
        public string? TotalMarks {  get; set; }
        public string? Notes {  get; set; }
        public string? File { get; set; }
        public int? AttendanceDays { get; set; }


        public Employee Employee { get; set; }
        public Course Course { get; set; }
    }
}
