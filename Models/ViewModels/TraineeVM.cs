using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class TraineeVM
    {
        public int EmployeeId { get; set; }
        [ValidateNever]
        [Display(Name = "رقم المؤسسة")]
        public string? EmployeeFoundationId { get; set; }
        [ValidateNever]
        [Display(Name = "اسم المتدرب")]
        public string? EmployeeName { get; set; }
        [ValidateNever]
        [Display(Name = "الوظيفة")]
        public string? Job { get; set; }
        [ValidateNever]
        [Display(Name = "مكان العمل")]
        public string? WorkPlace { get; set; }
        [ValidateNever]
        [Display(Name = "التقدير")]
        public Estimate? Estimate { get; set; }
        [ValidateNever]
        [Display(Name = "ملاحظات التنفيذ")]
        public string? Notes { get; set; }
        [ValidateNever]
        [Display(Name = "ملاحظات التقييم")]
        public string? SecondNotes { get; set; }
        [ValidateNever]
        [Display(Name = "ملف")]
        public string? File { get; set; }
        [ValidateNever] 
        [Display(Name = "عدد ايام الغياب")]
        public int? AbsenceDays { get; set; }
        [ValidateNever]
        [Display(Name = "الحضور والانصراف")]
        public int? AttendanceAndDeparture { get; set; }
        [ValidateNever]
        [Display(Name = "الالتزام ")]
        public int? AdherenceMark { get; set; }
        [ValidateNever]
        [Display(Name = "التفاعل")]
        public int? InteractionMark { get; set; }
        [ValidateNever]
        [Display(Name = "اداء الانشطة")]
        public int? ActivitiesMark { get; set; }
        [ValidateNever]
        [Display(Name = "اجمالي التقييم")]
        public int? TotalEvaluation { get; set; }
        [ValidateNever]
        [Display(Name = "درجة الامتحان التحريري")]
        public double? WrittenExam { get; set; }
        [ValidateNever]
        [Display(Name = "النتيجة الاجمالة")]
        public double? TotalMarks { get; set; }

        [ValidateNever]
        public int? CourseId { get; set; }
    }
}
