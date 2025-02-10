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
    public class CourseInstructorVM
    {
        [Required]
        [Display(Name = "المدرب")]
        public int InstructorId { get; set; }
        [ValidateNever]
        [Display(Name ="الملاحظات")]
        public string? CourseNotes { get; set; }
        [ValidateNever]
        [Display(Name = "التقييم")]
        public double? Rating { get; set; }
        [Required]
        [Display(Name = "الموضع")]
        public Position? Position { get; set; }
    }
}
