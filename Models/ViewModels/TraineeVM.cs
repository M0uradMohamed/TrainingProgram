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
        public string EmployeeFoundationId { get; set; }
        [ValidateNever]
        [Display(Name = "اسم المتدرب")]
        public string EmployeeName { get; set; }
        [ValidateNever]
        [Display(Name = "الوظيفة")]
        public string? Job { get; set; }
        [ValidateNever]
        [Display(Name = "مكان العمل")]
        public string? WorkPlace { get; set; }
        [Display(Name = "التقدير")]
        [ValidateNever]
        public Estimate? Estimate { get; set; }
        [Display(Name = "الملاحظات")]
        [ValidateNever]
        public string? Notes { get; set; }
        [Display(Name = "ملف")]
        [ValidateNever]
        public string? File { get; set; }
        [Display(Name = "عدد ايام الغياب")]
        [ValidateNever]
        public int? AbsenceDays { get; set; }
        [Display(Name = "الحضور والانصراف")]
        [ValidateNever]
        public int? AttendanceAndDeparture { get; set; }
        [Display(Name = "الالتزام ")]
        [ValidateNever]
        public int? AdherenceMark { get; set; }
        [Display(Name = "التفاعل")]
        [ValidateNever]
        public int? InteractionMark { get; set; }
        [Display(Name = "اداء الانشطة")]
        [ValidateNever]
        public int? ActivitiesMark { get; set; }
        [Display(Name = "اجمالي التقييم")]
        [ValidateNever]
        public int? TotalEvaluation { get; set; }
        [Display(Name = "درجة الامتحان التحريري")]
        [ValidateNever]
        public double? WrittenExam { get; set; }
        [Display(Name = "النتيجة الاجمالة")]
        [ValidateNever]
        public double? TotalMarks { get; set; }
    }
}
