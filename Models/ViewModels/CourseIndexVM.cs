using Models.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CourseIndexVM
    {
        public int Id { get; set; }
        [Display(Name= "أسم الدورة")]
        public string? Name { get; set; }
        [Display(Name = "القطاع المستهدف")]
        public string? TargetSector { get; set; }
        [Display(Name = "المشاركين")]
        public string? Participants { get; set; }
        [Display(Name = "مكان التنفيذ")]
        public string? ImplementationPlace { get; set; }
        [Display(Name = "عدد الايام")]
        public int? DaysCount { get; set; }
        [Display(Name = "الايام المنفذة")]
        public string? ImplementedDays { get; set; }
        [Display(Name = "تاريح البداية")]
        public DateOnly? BeginningDate { get; set; }
        [Display(Name = "تاريخ النهاية")]
        public DateOnly? EndingDate { get; set; }
        [Display(Name = "عدد المتدربين")]
        public int? TraineesNumber { get; set; }
        [Display(Name = "التكلفة")]
        public double? Cost { get; set; }
        [Display(Name = "مركز التنفيذ")]
        public string? ImplementedCenter { get; set; }
        [Display(Name = "عدد الساعات")]
        public int? HoursNumber { get; set; }
        [Display(Name = "نوع التنفيذ")]
        public string? ImplementationTypeName { get; set; }
        [Display(Name = "اجمالي التنفيذ")]
        public string? TotalImplementationName { get; set; }
        [Display(Name = "رقم القاعة")]
        public int? RoomNumber { get; set; }
        [Display(Name = "المادة العلمية")]
        public Material? Material { get; set; }
        [Display(Name = "نوعية الدورة")]
        public CourseType? CourseType { get; set; }
        [Display(Name = "التقييم")]
        public double? Rating { get; set; }
        [Display(Name = "شهر التنفيذ")]
        public ImplementationMonth? ImplementationMonth { get; set; }
        [Display(Name = "التكلفة الفعلية")]
        public double? ActualCost { get; set; }
        [Display(Name = "كود البرنامج")]
        public string? Code { get; set; }
        [Display(Name = "حالة البرنامج")]
        public Check? Check { get; set; }
        [Display(Name = "pdf ملف")]
        public string? PdfFile { get; set; }
        [Display(Name = "اسم مدخل البيانات")]
        public string? EnterName { get; set; }
        [Display(Name = "لينك المادة العلمية")]
        public string? Link { get; set; }
        [Display(Name = "اخصائي التقييم")]
        public string? RatingSpecialist { get; set; }
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        [Display(Name = "ملاحظات اخصائي التقييم")]
        public string? RatingSpecialistNotes { get; set; }
        [Display(Name = "ملاحظات المتدربين")]
        public string? TraineesNotes { get; set; }
        [Display(Name = "تقييم المتدربين")]
        public double? TraineesRating { get; set; }
        [Display(Name = "طبيعة الدورة")]
        public string? CourseNatureName { get; set; }
        [Display(Name = "أخصائي التنفيذ")]
        public string? TrainingSpecialistName { get; set; }
        [Display(Name = "المدرب الاول")]
        public string? FirstInstructorName { get; set; }
        [Display(Name = "المدرب الثاني")]
        public string? SecondInstructorName { get; set; }
        [Display(Name = "المدرب الثالث")]
        public string? ThirdInstructorName { get; set; }
        [Display(Name = "المدرب الرابع")]
        public string? ForthInstructorName { get; set; }


    }
}
