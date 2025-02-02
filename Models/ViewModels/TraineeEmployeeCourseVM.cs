using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class TraineeEmployeeCourseVM
    {
        public string? FoundationId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Job { get; set; }
        public string? Department { get; set; }
        public string? SectorName { get; set; }
        public string? WorkPlace { get; set; }
        public string? CourseName { get; set; }
        public DateOnly? BeginningDate { get; set; }
        public DateOnly? EndingDate { get; set; }
        public double? TotalMarks { get; set; }


    }
}
