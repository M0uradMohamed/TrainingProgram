using Models.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CourseInstructor
    {
        public int CourseId { get; set; }
        public int InstructorId { get; set; }
        public string? CourseNotes { get; set; }
        public double? Rating { get; set; }
        public Position Position { get; set; }

        public Course? Course { get; set; }
        public Instructor? Instructor { get; set; }
        

    }
}
