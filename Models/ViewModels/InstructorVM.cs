using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class InstructorVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "رقم المؤسسة")]
        public string? FoundationId { get; set; }
        [Required]
        [Display(Name = "الاسم")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "تاريخ الميلاد")]
        public DateOnly BirthDate { get; set; }
        [ValidateNever]
        [Display(Name = "تاريخ التعيين")]
        public string? HiringDate { get; set; }
        [ValidateNever]
        [Display(Name = "الدرجة")]
        public int? DegreeId { get; set; }
        [ValidateNever]
        [Display(Name = "الادارة العامة")]
        public string? Department { get; set; }
        [ValidateNever]
        [Display(Name = "القطاع")]
        public int? SectorId { get; set; }
        [ValidateNever]
        [Display(Name = "مكان العمل")]
        public string? WorkPlace { get; set; }
        [ValidateNever]
        [Display(Name = "المؤهل")]
        public string? Major { get; set; }
        [ValidateNever]
        [Display(Name = "تاريخ التخرج")]
        public string? GraduationeDate { get; set; }
        [ValidateNever]
        [Display(Name = "الدرجة العلمية")]
        public string? AcademicDegree { get; set; }
        [ValidateNever]
        [Display(Name = "تاريخ الحصور عليها")]
        public string? AcademicDegreeeDate { get; set; }
        [ValidateNever]
        [Display(Name = "رقم الهاتف")]
        public string? PhoneNumber { get; set; }
        [ValidateNever]
        [Display(Name = "رقم هاتف اخر")]
        public string? OtherPhoneNumber { get; set; }
        [ValidateNever]
        [Display(Name = "الاميل")]
        public string? Email { get; set; }
    }
}
