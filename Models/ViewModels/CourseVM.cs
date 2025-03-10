﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.EnumClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CourseVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "أسم الدورة")]
        public string? Name { get; set; }
        [ValidateNever]
        [Display(Name = "القطاع المستهدف")]
        public string? TargetSector { get; set; }
        [ValidateNever]
        [Display(Name = "المشاركين")]
        public string? Participants { get; set; }
        [ValidateNever]
        [Display(Name = "مكان التنفيذ")]
        public string? ImplementationPlace { get; set; }
        [ValidateNever]
        [Display(Name = "عدد الايام")]
        public int? DaysCount { get; set; }
        [ValidateNever]
        [Display(Name = "الايام المنفذة")]
        public string? ImplementedDays { get; set; }
        [Required]
        [Display(Name = "تاريح البداية")]
        public DateOnly? BeginningDate { get; set; }
        [Required]
        [Display(Name = "تاريخ النهاية")]
        public DateOnly? EndingDate { get; set; }
        [ValidateNever]
        [Display(Name = "عدد المتدربين")]
        public int? TraineesNumber { get; set; }
        [ValidateNever]
        [Display(Name = "التكلفة")]
        public double? Cost { get; set; }
        [ValidateNever]
        [Display(Name = "مركز التنفيذ")]
        public string? ImplementedCenter { get; set; }
        [ValidateNever]
        [Display(Name = "عدد الساعات")]
        public int? HoursNumber { get; set; }
        [ValidateNever]
        [Display(Name = "نوع التنفيذ")]
        public int? ImplementationTypeId { get; set; }
        [ValidateNever]
        [Display(Name = "اجمالي التنفيذ")]
        public int? TotalImplementationId { get; set; }
        [ValidateNever]
        [Display(Name = "رقم القاعة")]
        public int? RoomNumber { get; set; }
        [ValidateNever]
        [Display(Name = "المادة العلمية")]
        public Material? Material { get; set; }
        [ValidateNever]
        [Display(Name = "نوعية الدورة")]
        public CourseType? CourseType { get; set; }
        [ValidateNever]
        [Display(Name = "التقييم")]
        public double? Rating { get; set; }
        [ValidateNever]
        [Display(Name = "شهر التنفيذ")]
        public ImplementationMonth? ImplementationMonth { get; set; }
        [ValidateNever]
        [Display(Name = "التكلفة الفعلية")]
        public double? ActualCost { get; set; }
        [ValidateNever]
        [Display(Name = "كود البرنامج")]
        public string? Code { get; set; }
        [Required]
        [Display(Name = "حالة البرنامج")]
        public Check? Check { get; set; }
        [ValidateNever]
        [Display(Name = "pdf ملف")]
        public string? PdfFile { get; set; }
        [ValidateNever]
        [Display(Name = "لينك المادة العلمية")]
        public string? Link { get; set; }
        [ValidateNever]
        [Display(Name = "اخصائي التقييم")]
        public string? RatingSpecialist { get; set; }
        [ValidateNever]
        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        [ValidateNever]
        [Display(Name = "ملاحظات اخصائي التقييم")]
        public string? RatingSpecialistNotes { get; set; }
        [ValidateNever]
        [Display(Name = "ملاحظات المتدربين")]
        public string? TraineesNotes { get; set; }
        [ValidateNever]
        [Display(Name = "تقييم المتدربين")]
        public double? TraineesRating { get; set; }
        [ValidateNever]
        [Display(Name = "طبيعة الدورة")]
        public int? CourseNatureId { get; set; }
        [ValidateNever]
        [Display(Name = "أخصائي تدريب")]
        public int? TrainingSpecialistId { get; set; }
        [ValidateNever]
        [Display(Name = "الجهة الممولة")]
        public string? FundingEntity { get; set; }

    }
}
