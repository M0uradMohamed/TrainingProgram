﻿using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.EnumClasses;
using Models.StaticData;
using Models.ViewModels;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TraineeController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly ITraineeRepository traineeRepository;
        private readonly IEmployeeRepository employeeRepository;

        public TraineeController(ICourseRepository courseRepository, ITraineeRepository traineeRepository
            , IEmployeeRepository employeeRepository)
        {
            this.courseRepository = courseRepository;
            this.traineeRepository = traineeRepository;
            this.employeeRepository = employeeRepository;
        }
        public IActionResult index(string? EmployeeNumber, string? CourseName, Estimate? Estimate, int page = 1)
        {
            var qtrainees = traineeRepository.Get(includeProps: [e => e.Employee!, e => e.Course!, e => e.Employee!.Sector!]);

            if (CourseName != null)
            {
                qtrainees = qtrainees.Where(e => e.Course!.Name!.Contains(CourseName.Trim()));
            }

            if (EmployeeNumber != null)
            {

                List<string> employeeList = EmployeeNumber
           .Split(new char[] { '+', ',' }, StringSplitOptions.RemoveEmptyEntries)
           .Select(e => e.Trim())
           .ToList();

                qtrainees = qtrainees.Where(e => employeeList.Any(emp => e.Employee!.FoundationId!.Contains(emp)));
            }
            if (Estimate != null)
            {
                qtrainees = qtrainees.Where(e => e.Estimate == Estimate);
            }

            IQueryable<TraineeEmployeeCourseVM> trainees = qtrainees.Select(e => new TraineeEmployeeCourseVM
            {
                CourseId = e.CourseId,
                FoundationId = e.Employee!.FoundationId,
                EmployeeName = e.Employee.Name,
                Job = e.Employee.Job,
                Department = e.Employee.Department,
                SectorName = e.Employee.Sector!.Name,
                WorkPlace = e.Employee.WorkPlace,
                CourseName = e.Course!.Name,
                BeginningDate = e.Course.BeginningDate,
                EndingDate = e.Course.EndingDate,
                TotalMarks = e.TotalMarks
            });
            double totalPages = Math.Ceiling((double)trainees.Count() / 50);
            trainees = trainees.Skip((page - 1) * 50).Take(50);

            ViewBag.Pages = new { page, totalPages };

            var search = new { EmployeeNumber, CourseName, Estimate };

            ViewBag.Search = search;
            ViewBag.Estimate = StaticData.estimate;

            return View(trainees.ToList());
        }
        public IActionResult Course(int id, int page = 1)
        {

            var Iscourse = courseRepository.Get(expression: e => e.Id == id).Any();

            if (!Iscourse)
            {
                return RedirectToAction("Notfound", "Home");

            }
            var qcourse = courseRepository.Get(expression: e => e.Id == id);
            var course = qcourse.Include(e => e.CoursesInstructors).ThenInclude(e => e.Instructor)
         .Select(e => new
         {
             e.Id,
             e.Name,
             e.BeginningDate,
             e.EndingDate,
             e.ImplementationPlace,
             Instructor = e.CoursesInstructors.Where(e => e.Position == Position.First).Select(e => e.Instructor!.Name).FirstOrDefault()

         }).FirstOrDefault();

            ViewBag.Course = course;


            var trainees = traineeRepository.Get(expression: e => e.CourseId == id, includeProps: [e => e.Employee!])
       .Select(e => new TraineeVM
       {
           EmployeeId = e.EmployeeId,
           EmployeeFoundationId = e.Employee!.FoundationId,
           EmployeeName = e.Employee.Name,
           Job = e.Employee.Job,
           WorkPlace = e.Employee.WorkPlace,
           Estimate = e.Estimate,
           Notes = e.Notes,
           SecondNotes = e.SecondNotes,
           File = e.File,
           AbsenceDays = e.AbsenceDays,
           AttendanceAndDeparture = e.AttendanceAndDeparture,
           AdherenceMark = e.AdherenceMark,
           InteractionMark = e.InteractionMark,
           ActivitiesMark = e.ActivitiesMark,
           TotalEvaluation = e.TotalEvaluation,
           WrittenExam = e.WrittenExam,
           TotalMarks = e.TotalMarks
       });

            double totalPages = Math.Ceiling((double)trainees.Count() / 5);
            trainees = trainees.Skip((page - 1) * 5).Take(5);

            ViewBag.Pages = new { page, totalPages };
            return View(trainees.ToList());
        }

        public IActionResult Create(int id)
        {
            var IsCourse = courseRepository.Get(expression: e => e.Id == id).Any();

            if (!IsCourse)
            {
                return RedirectToAction("Notfound", "Home");

            }

            var employees = employeeRepository.Get().Select(e => new
            { e.Id, e.FoundationId, e.Name, e.Job, e.WorkPlace }).ToList();

            ViewBag.Employees = employees;
            ViewBag.CourseId = id;
            ViewBag.Estimate = StaticData.estimate;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, TraineeVM traineeVM, IFormFile file)
        {
            ModelState.Remove(nameof(file));
            if (employeeRepository.Get(expression: e => e.Id == traineeVM.EmployeeId).Any()
                && courseRepository.Get(expression: e => e.Id == id).Any())
            {


                if (traineeRepository.Get(expression: e => e.CourseId == id && e.EmployeeId == traineeVM.EmployeeId).Any())
                {
                    ModelState.AddModelError(nameof(traineeVM.EmployeeId), "هذا المتدرب موجود بالفعل في هذا البرنامج");

                    ViewBag.Employees = employeeRepository.Get().Select(e => new
                    { e.Id, e.FoundationId, e.Name, e.Job, e.WorkPlace }).ToList();
                    ViewBag.Estimate = StaticData.estimate;
                    ViewBag.CourseId = id;

                    return View(traineeVM);
                }
                var course = courseRepository.Get(expression: e => e.Id == id, tracked: false)
                    .Select(e => new { e.BeginningDate, e.EndingDate }).FirstOrDefault();

                var similarTrinee = traineeRepository.Get(includeProps: [e => e.Course!], e => e.EmployeeId == traineeVM.EmployeeId)
                    .Any(e => course!.BeginningDate <= e.Course!.EndingDate &&
                                  course.EndingDate >= e.Course.BeginningDate);

                if (similarTrinee)
                {
                    ModelState.AddModelError(nameof(traineeVM.EmployeeId), "هذا المتدرب موجود بالفعل في برنامج يتعارض  موعده مع برنامج اخر");

                    ViewBag.Employees = employeeRepository.Get().Select(e => new
                    { e.Id, e.FoundationId, e.Name, e.Job, e.WorkPlace }).ToList();
                    ViewBag.Estimate = StaticData.estimate;
                    ViewBag.CourseId = id;

                    return View(traineeVM);
                }

                if (file != null)
                {

                    if (Path.GetExtension(file.FileName).ToLower() != ".pdf" && Path.GetExtension(file.FileName).ToLower() != ".jpg"
                      && Path.GetExtension(file.FileName).ToLower() != ".png")
                    {
                        ModelState.AddModelError(nameof(traineeVM.File), "هذا الملف غير مدعوم");

                        ViewBag.Employees = employeeRepository.Get().Select(e => new
                        { e.Id, e.FoundationId, e.Name, e.Job, e.WorkPlace }).ToList();
                        ViewBag.Estimate = StaticData.estimate;
                        ViewBag.CourseId = id;

                        return View(traineeVM);
                    }
                }
                var trainee = new Trainee()
                {
                    CourseId = id,
                    EmployeeId = traineeVM.EmployeeId,
                    Estimate = traineeVM.Estimate,
                    Notes = traineeVM.Notes,
                    SecondNotes = traineeVM.SecondNotes,
                    AbsenceDays = traineeVM.AbsenceDays ?? 0,
                    AttendanceAndDeparture = traineeVM.AttendanceAndDeparture ?? 0,
                    AdherenceMark = traineeVM.AdherenceMark ?? 0,
                    InteractionMark = traineeVM.InteractionMark ?? 0,
                    ActivitiesMark = traineeVM.ActivitiesMark ?? 0,
                    WrittenExam = traineeVM.WrittenExam ?? 0
                };

                trainee.TotalEvaluation = (trainee.AdherenceMark + trainee.InteractionMark + trainee.ActivitiesMark);
                trainee.TotalMarks = (trainee.TotalEvaluation + trainee.WrittenExam + trainee.AttendanceAndDeparture);


                traineeRepository.Create(trainee);
                traineeRepository.Commit();

                if (file != null)
                {
                    string fileName = $"file{trainee.EmployeeId}".ToString();
                    string extension = Path.GetExtension(file.FileName);


                    fileName += extension;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\TraineesFiles\\Course{trainee.CourseId}", fileName);

                    string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\TraineesFiles\\Course{trainee.CourseId}");


                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    trainee.File = fileName;

                    traineeRepository.Edit(trainee);
                    traineeRepository.Commit();
                }
                var name = traineeRepository.Get(expression: e => e.EmployeeId == trainee.EmployeeId, includeProps: [e => e.Employee!], tracked: false)
                    .Select(e => e.Employee!.Name).FirstOrDefault();

                TempData["success"] = $"  تم اضافة المتدرب بنجاح , {name} ";

                ViewBag.Employees = employeeRepository.Get().Select(e => new
                { e.Id, e.FoundationId, e.Name, e.Job, e.WorkPlace }).ToList();
                ViewBag.Estimate = StaticData.estimate;
                ViewBag.CourseId = id;
                ModelState.Clear();
                return View();


            }
            ModelState.AddModelError(nameof(traineeVM.EmployeeId), "employee is requied");

            ViewBag.Employees = employeeRepository.Get().Select(e => new
            { e.Id, e.FoundationId, e.Name, e.Job, e.WorkPlace }).ToList();
            ViewBag.Estimate = StaticData.estimate;
            ViewBag.CourseId = id;

            return View(traineeVM);
        }
        public IActionResult Edit(int id, int EmployeeId)
        {

            var IsTrainee = traineeRepository.Get(expression: e => e.CourseId == id && e.EmployeeId == EmployeeId).Any();

            if (!IsTrainee)
            {
                return RedirectToAction("Notfound", "Home");

            }

            var employee = employeeRepository.Get(expression: e => e.Id == EmployeeId).Select(e => new
            { e.FoundationId, e.Name, e.Job, e.WorkPlace }).FirstOrDefault();

            var traineeVM = traineeRepository.Get(expression: e => e.CourseId == id && e.EmployeeId == EmployeeId,
                 includeProps: [e => e.Course!, e => e.Employee!]).Select(e => new TraineeVM
                 {
                     CourseId = e.CourseId,
                     EmployeeId = e.EmployeeId,
                     Estimate = e.Estimate,
                     AbsenceDays = e.AbsenceDays,
                     AttendanceAndDeparture = e.AttendanceAndDeparture,
                     ActivitiesMark = e.ActivitiesMark,
                     AdherenceMark = e.AdherenceMark,
                     InteractionMark = e.InteractionMark,
                     Notes = e.Notes,
                     SecondNotes = e.SecondNotes,
                     File = e.File,
                     TotalMarks = e.TotalMarks,
                     WrittenExam = e.WrittenExam,
                     TotalEvaluation = e.TotalEvaluation

                 }).FirstOrDefault();


            ViewBag.Employee = employee;
            ViewBag.Estimate = StaticData.estimate;

            return View(traineeVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TraineeVM traineeVM, IFormFile uploadedFile)
        {
            ModelState.Remove(nameof(uploadedFile));
            if (traineeRepository.Get(expression: e => e.EmployeeId == traineeVM.EmployeeId && e.CourseId == id, tracked: false).Any())
            {

                if (uploadedFile != null)
                {

                    if (Path.GetExtension(uploadedFile.FileName).ToLower() != ".pdf" && Path.GetExtension(uploadedFile.FileName).ToLower() != ".jpg"
                      && Path.GetExtension(uploadedFile.FileName).ToLower() != ".png")
                    {
                        ModelState.AddModelError(nameof(traineeVM.File), "هذا الملف غير مدعوم");

                        return View(traineeVM);
                    }
                }
                var trainee = new Trainee()
                {
                    CourseId = id,
                    EmployeeId = traineeVM.EmployeeId,
                    Estimate = traineeVM.Estimate,
                    Notes = traineeVM.Notes,
                    SecondNotes = traineeVM.SecondNotes,
                    File = traineeVM.File,
                    AbsenceDays = traineeVM.AbsenceDays ?? 0,
                    AttendanceAndDeparture = traineeVM.AttendanceAndDeparture ?? 0,
                    AdherenceMark = traineeVM.AdherenceMark ?? 0,
                    InteractionMark = traineeVM.InteractionMark ?? 0,
                    ActivitiesMark = traineeVM.ActivitiesMark ?? 0,
                    WrittenExam = traineeVM.WrittenExam ?? 0
                };
                trainee.TotalEvaluation = (trainee.AdherenceMark + trainee.InteractionMark + trainee.ActivitiesMark);
                trainee.TotalMarks = (trainee.TotalEvaluation + trainee.WrittenExam + trainee.AttendanceAndDeparture);

                traineeRepository.Edit(trainee);
                traineeRepository.Commit();

                if (uploadedFile != null)
                {
                    string fileName = $"file{trainee.EmployeeId}".ToString();
                    string extension = Path.GetExtension(uploadedFile.FileName);


                    fileName += extension;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\TraineesFiles\\Course{trainee.CourseId}", fileName);

                    string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\TraineesFiles\\Course{trainee.CourseId}");


                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    if (traineeVM.File != null)
                    {

                        string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\TraineesFiles\\Course{id}", traineeVM.File);

                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }


                    using (var stream = System.IO.File.Create(filePath))
                    {
                        uploadedFile.CopyTo(stream);
                    }
                    trainee.File = fileName;

                    traineeRepository.Edit(trainee);
                    traineeRepository.Commit();
                }

                return RedirectToAction("Course", new { id = $"{id}" });


            }
            return RedirectToAction("Notfound", "Home");

        }
        [HttpPost]
        public IActionResult Delete(int id, int EmployeeId)
        {
            var trainee = traineeRepository.Get(expression: m => m.CourseId == id && m.EmployeeId == EmployeeId).FirstOrDefault();
            if (trainee != null)
            {
                if (trainee.File != null)
                {

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\TraineesFiles\\Course{trainee.CourseId}", trainee.File);

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                traineeRepository.Delete(trainee);
                traineeRepository.Commit();
                return RedirectToAction("course", new { id = $"{id}" });
            }
            return RedirectToAction("Notfound", "Home");
        }
       // public IActionResult print(int id)
       // {
       //     var Iscourse = courseRepository.Get(expression: e => e.Id == id).Any();

       //     if (!Iscourse)
       //     {
       //         return RedirectToAction("Notfound", "Home");

       //     }
       //     var course = courseRepository.Get(expression: e => e.Id == id /*, includeProps: [e => e.PrimaryInstructor]*/).Select(e => new
       //     {
       //         e.Id,
       //         e.Name,
       //         BeginningDate = e.BeginningDate.ToString(),
       //         EndingDate = e.EndingDate.ToString(),

       //     }).FirstOrDefault();
       //     var courses = new List<object>()
       //     {
       //         course!
       //     };


       //     var trainees = traineeRepository.Get(expression: e => e.CourseId == id, includeProps: [e => e.Employee!])
       //.Select(e => new
       //{

       //    Id = e.Employee!.FoundationId,
       //    Name = e.Employee.Name

       //}).ToList();

       //     // string path = Path.Combine(webHostEnvironment.WebRootPath + @"\Reports\SectorReport.rdlc");
       //     string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Reports\\TraineesReport.rdlc");


       //     // Dictionary<string, string> parmaeters = new Dictionary<string, string>();
       //     // parmaeters.Add("Ahmed", "شركة المياة");



       //     LocalReport localreport = new LocalReport(path);
       //     localreport.AddDataSource("CourseTrainee", courses);
       //     localreport.AddDataSource("TraineeDataSet", trainees);

       //     var report = localreport.Execute(RenderType.Pdf, 1, null, "");




       //     return File(report.MainStream, "application/pdf");
       // }
        public IActionResult DownloadFile(string fileName, int id)
        {
            if (fileName != null)
            {

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\TraineesFiles\\Course{id}", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "application/pdf", fileName);
                }
            }
            TempData["noFile"] = "there is no file to download";
            return View();

        }
    }
}
