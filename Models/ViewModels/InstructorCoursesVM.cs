using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class InstructorCoursesVM
    {
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public DateOnly? BeginningDate { get; set; }
        public DateOnly? EndingDate { get; set; }
        public double? Rating { get; set; }
    }
}
