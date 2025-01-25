using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EnumClasses;

namespace Models
{
    public class Trainee
    {
        public int CourseId { get; set; }
        public int EmployeeId { get; set; }
        //enum
        public Estimate? Estimate {  get; set; }
        public string? Notes {  get; set; }
        public string? File { get; set; }

        //--------------------------\\
        public int? AbsenceDays { get; set; }
        public int? AttendanceAndDeparture { get; set; }
        public int? AdherenceMark { get; set; }
        public int? InteractionMark { get; set; }
        public int? ActivitiesMark { get; set; }
        public int? TotalEvaluation { get; set; }
        public double? WrittenExam { get; set; }
        public double? TotalMarks {  get; set; }


        public Employee Employee { get; set; }
        public Course Course { get; set; }
    }
}
