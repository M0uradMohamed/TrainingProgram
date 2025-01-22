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
    public class EmployeeVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="رقم المؤسسة")]
        public string? FoundationId { get; set; }
        [Required]
        [Display(Name = "الاسم")]
        public string? Name { get; set; }
        [ValidateNever]
        [Display(Name = "النوع")]
        public Gender? Gender { get; set; }
        [ValidateNever]
        [Display(Name = "المؤهل")]
        public string? Major { get; set; }
        [ValidateNever]
        [Display(Name = "الوظيفة")]
        public string? Job { get; set; }
        [ValidateNever]
        [Display(Name = "الادارة العامة")]
        public string? Department { get; set; }
        [ValidateNever]
        [Display(Name = "القطاع")]
        public int? SectorId { get; set; }
        [ValidateNever]
        [Display(Name = "الدرجة")]
        public int? DegreeId { get; set; }
        [ValidateNever]
        [Display(Name = "مكان العمل")]
        public string? WorkPlace { get; set; }
        [ValidateNever]
        [Display(Name = "رقم الهاتف")]
        public string? PhoneNumber { get; set; }
        [ValidateNever]
        [Display(Name = "التبعية")]
        public Belong? Belong { get; set; }
        [ValidateNever]
        [Display(Name = "اسم الشركة للغير نابع")]
        public string? CompanyNameForForeign { get; set; }
    }
}
