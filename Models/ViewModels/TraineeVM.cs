using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.EnumClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class TraineeVM
    {
        [ValidateNever]
        public string EmployeeFoundationId { get; set; }
        [ValidateNever]
        public string EmployeeName { get; set; }
        [ValidateNever]
        public string? Job { get; set; }
        [ValidateNever]
        public string? WorkPlace { get; set; }
        public Estimate? Estimate { get; set; } 
        public string? Notes { get; set; }
        public string? File { get; set; }
        public int? AbsenceDays { get; set; }
        public int? AttendanceAndDeparture { get; set; }
        public int? AdherenceMark { get; set; }
        public int? InteractionMark { get; set; }
        public int? ActivitiesMark { get; set; }
        public int? TotalEvaluation { get; set; }
        public double? WrittenExam { get; set; }
        public double? TotalMarks { get; set; }
    }
}
