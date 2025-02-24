﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models;
using DataAccess.Repository.IRepository;
using Models.ViewModels;
using System.Numerics;
using Models.StaticData;
using System.Xml.Linq;
using DataAccess.Repository;
using Models.EnumClasses;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using Microsoft.VisualBasic;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CourseController : Controller
    {

        private readonly ICourseRepository courseRepository;
        private readonly IInstructorRepository instructorRepository;
        private readonly ICourseNatureRepository courseNatureRepository;
        private readonly ITotalImplementationRepository totalImplementationRepository;
        private readonly IImplementationTypeRepository implementationTypeRepository;
        private readonly ICourseInstructorRepository courseInstructorRepository;
        private readonly ITrainingSpecialistRepository trainingSpecialistRepository;
        private readonly ISectorRepository sectorRepository;


        public CourseController(ICourseRepository courseRepository, IInstructorRepository instructorRepository
            , ICourseNatureRepository courseNatureRepository, ITotalImplementationRepository totalImplementationRepository
            , IImplementationTypeRepository implementationTypeRepository, ICourseInstructorRepository courseInstructorRepository,
            ITrainingSpecialistRepository trainingSpecialistRepository, ISectorRepository sectorRepository
     )
        {

            this.courseRepository = courseRepository;
            this.instructorRepository = instructorRepository;
            this.courseNatureRepository = courseNatureRepository;
            this.totalImplementationRepository = totalImplementationRepository;
            this.implementationTypeRepository = implementationTypeRepository;
            this.courseInstructorRepository = courseInstructorRepository;
            this.trainingSpecialistRepository = trainingSpecialistRepository;
            this.sectorRepository = sectorRepository;

        }
        // GET: Manage/Course
        public IActionResult Index(string? Name, DateOnly? BeginningDate, DateOnly? EndingDate, string? ImplementationPlace
            , string? ImplementedCenter, int? ImplementationTypeId, int? TotalImplementationId, Check? Check, int? CourseNatureId
            , string? Instructor, bool Sort = false, int page = 1)
        {
            IQueryable<Course> qcourses = courseRepository.Get(includeProps: [
                e=>e.CourseNature!, e=>e.TotalImplementation! ,e=>e.ImplementationType! ,e=>e.TrainingSpecialist!
                ]);
            qcourses = qcourses.Include(e => e.CoursesInstructors).ThenInclude(e => e.Instructor);

            if (Name != null)
            {
                qcourses = qcourses.Where(e => e.Name!.Contains(Name.TrimStart().TrimEnd()));
            }
            if (BeginningDate != null)
            {
                qcourses = qcourses.Where(e => e.BeginningDate >= BeginningDate);
            }
            if (EndingDate != null)
            {
                qcourses = qcourses.Where(e => e.EndingDate <= EndingDate);
            }
            if (ImplementationPlace != null)
            {
                qcourses = qcourses.Where(e => e.ImplementationPlace!.Contains(ImplementationPlace.TrimStart().TrimEnd()));
            }
            if (ImplementedCenter != null)
            {
                qcourses = qcourses.Where(e => e.ImplementedCenter!.Contains(ImplementedCenter.TrimStart().TrimEnd()));
            }
            if (ImplementationTypeId != null)
            {
                qcourses = qcourses.Where(e => e.ImplementationType!.Id == ImplementationTypeId);
            }
            if (TotalImplementationId != null)
            {
                qcourses = qcourses.Where(e => e.TotalImplementation!.Id == TotalImplementationId);
            }
            if (Check != null)
            {
                qcourses = qcourses.Where(e => e.Check == Check);
            }
            if (CourseNatureId != null)
            {
                qcourses = qcourses.Where(e => e.CourseNature!.Id == CourseNatureId);
            }
            if (Instructor != null)
            {
                qcourses = qcourses.Where(e => e.CoursesInstructors.Any(c => c.Instructor!.Name!.Contains(Instructor.TrimStart().TrimEnd())));
            }


            IQueryable<CourseIndexVM> courses = qcourses.Select(e => new CourseIndexVM
            {
                Id = e.Id,
                Name = e.Name,
                TargetSector = e.TargetSector,
                Participants = e.Participants,
                ImplementationPlace = e.ImplementationPlace,
                DaysCount = e.DaysCount,
                ImplementedDays = e.ImplementedDays,
                BeginningDate = e.BeginningDate,
                EndingDate = e.EndingDate,
                TraineesNumber = e.TraineesNumber,
                Cost = e.Cost,
                ImplementedCenter = e.ImplementedCenter,
                HoursNumber = e.HoursNumber,
                ImplementationTypeName = e.ImplementationType!.Name,
                TotalImplementationName = e.TotalImplementation!.Name,
                RoomNumber = e.RoomNumber,
                Material = e.Material,
                CourseType = e.CourseType,
                Rating = e.Rating,
                ImplementationMonth = e.ImplementationMonth,
                ActualCost = e.ActualCost,
                Code = e.Code,
                Check = e.Check,
                PdfFile = e.PdfFile,
                EnterName = e.EnterName,
                Link = e.Link,
                RatingSpecialist = e.RatingSpecialist,
                Notes = e.Notes,
                RatingSpecialistNotes = e.RatingSpecialistNotes,
                TraineesNotes = e.TraineesNotes,
                TraineesRating = e.TraineesRating,
                CourseNatureName = e.CourseNature!.Name,
                TrainingSpecialistName = e.TrainingSpecialist!.Name,
                FundingEntity = e.FundingEntity,
                FirstInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.First).Select(e => e.Instructor!.Name).FirstOrDefault(),
                SecondInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Second).Select(e => e.Instructor!.Name).FirstOrDefault(),
                ThirdInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Third).Select(e => e.Instructor!.Name).FirstOrDefault(),
                ForthInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Fourth).Select(e => e.Instructor!.Name).FirstOrDefault()

            });

            if (!Sort)
                courses = courses.OrderBy(e => e.BeginningDate).ThenBy(e => e.EndingDate);
            else
                courses = courses.OrderByDescending(e => e.BeginningDate).ThenByDescending(e => e.EndingDate);

            var Search = new
            {
                Name,
                BeginningDate,
                EndingDate,
                ImplementationPlace,
                ImplementedCenter,
                ImplementationTypeId,
                TotalImplementationId,
                Check,
                CourseNatureId,
                Instructor,
                Sort
            };

            double totalPages = Math.Ceiling((double)courses.Count() / 30);
            courses = courses.Skip((page - 1) * 30).Take(30);

            ViewBag.Pages = new { page, totalPages };


            ViewBag.Search = Search;
            ViewBag.ImplementationType = implementationTypeRepository.Get().ToList();
            ViewBag.TotalImplementation = totalImplementationRepository.Get().ToList();
            ViewBag.CourseNature = courseNatureRepository.Get().ToList();
            ViewBag.Check = StaticData.check;


            return View(courses.ToList());
        }

        // GET: Manage/Course/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            IQueryable<Course> qcourse = courseRepository.Get(expression: e => e.Id == id, includeProps: [
                  e=>e.CourseNature!, e=>e.TotalImplementation! ,e=>e.ImplementationType! ,e=>e.TrainingSpecialist!
                  ]);
            IQueryable<CourseIndexVM> course = qcourse.Include(e => e.CoursesInstructors).ThenInclude(e => e.Instructor).Select(e => new CourseIndexVM
            {
                Id = e.Id,
                Name = e.Name,
                TargetSector = e.TargetSector,
                Participants = e.Participants,
                ImplementationPlace = e.ImplementationPlace,
                DaysCount = e.DaysCount,
                ImplementedDays = e.ImplementedDays,
                BeginningDate = e.BeginningDate,
                EndingDate = e.EndingDate,
                TraineesNumber = e.TraineesNumber,
                Cost = e.Cost,
                ImplementedCenter = e.ImplementedCenter,
                HoursNumber = e.HoursNumber,
                ImplementationTypeName = e.ImplementationType!.Name,
                TotalImplementationName = e.TotalImplementation!.Name,
                RoomNumber = e.RoomNumber,
                Material = e.Material,
                CourseType = e.CourseType,
                Rating = e.Rating,
                ImplementationMonth = e.ImplementationMonth,
                ActualCost = e.ActualCost,
                Code = e.Code,
                Check = e.Check,
                PdfFile = e.PdfFile,
                EnterName = e.EnterName,
                Link = e.Link,
                RatingSpecialist = e.RatingSpecialist,
                Notes = e.Notes,
                RatingSpecialistNotes = e.RatingSpecialistNotes,
                TraineesNotes = e.TraineesNotes,
                TraineesRating = e.TraineesRating,
                CourseNatureName = e.CourseNature!.Name,
                TrainingSpecialistName = e.TrainingSpecialist!.Name,
                FundingEntity = e.FundingEntity,
                FirstInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.First).Select(e => e.Instructor!.Name).FirstOrDefault(),
                SecondInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Second).Select(e => e.Instructor!.Name).FirstOrDefault(),
                ThirdInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Third).Select(e => e.Instructor!.Name).FirstOrDefault(),
                ForthInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Fourth).Select(e => e.Instructor!.Name).FirstOrDefault()

            });


            if (course == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            return View(course.FirstOrDefault());
        }

        // GET: Manage/Course/Create
        public IActionResult Create()
        {
            ViewBag.CourseNature = courseNatureRepository.Get().ToList();
            ViewBag.TotalImplementation = totalImplementationRepository.Get().ToList();
            ViewBag.ImplementationType = implementationTypeRepository.Get().ToList();
            ViewBag.Material = StaticData.material;
            ViewBag.CourseType = StaticData.courseType;
            ViewBag.ImplementationMonth = StaticData.implementationMonth;
            ViewBag.TrainingSpecialist = trainingSpecialistRepository.Get().ToList();
            ViewBag.Check = StaticData.check;


            return View();
        }

        // POST: Manage/Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseVM courseVM, IFormFile file)
        {
            ModelState.Remove(nameof(file));
            //  ModelState.Remove(nameof(courseVM.instuctorsIds));
            if (ModelState.IsValid)
            {
                if (courseVM.BeginningDate! > courseVM.EndingDate)
                {
                    ModelState.AddModelError(string.Empty, "تاريخ البداية لا يمكن ان يكون بعد تاريخ النهاية");

                    ViewBag.CourseNature = courseNatureRepository.Get().ToList();
                    ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
                    ViewBag.TotalImplementation = totalImplementationRepository.Get().ToList();
                    ViewBag.ImplementationType = implementationTypeRepository.Get().ToList();
                    ViewBag.Material = StaticData.material;
                    ViewBag.CourseType = StaticData.courseType;
                    ViewBag.ImplementationMonth = StaticData.implementationMonth;
                    ViewBag.TrainingSpecialist = trainingSpecialistRepository.Get().ToList();
                    ViewBag.Check = StaticData.check;


                    return View(courseVM);
                }


                if (file != null)
                {

                    if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
                    {
                        ModelState.AddModelError(nameof(courseVM.PdfFile), "يسمح برفع ملفات pdf فقط");

                        ViewBag.CourseNature = courseNatureRepository.Get().ToList();
                        ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
                        ViewBag.TotalImplementation = totalImplementationRepository.Get().ToList();
                        ViewBag.ImplementationType = implementationTypeRepository.Get().ToList();
                        ViewBag.Material = StaticData.material;
                        ViewBag.CourseType = StaticData.courseType;
                        ViewBag.ImplementationMonth = StaticData.implementationMonth;
                        ViewBag.TrainingSpecialist = trainingSpecialistRepository.Get().ToList();
                        ViewBag.Check = StaticData.check;

                        return View(courseVM);
                    }
                }

                var course = new Course()
                {
                    Name = MethodsCheck.chechName(courseVM.Name),
                    DaysCount = courseVM.DaysCount,
                    CourseType = courseVM.CourseType,
                    Check = courseVM.Check,
                    Code = MethodsCheck.chechName(courseVM.Code),
                    ImplementedDays = MethodsCheck.chechName(courseVM.ImplementedDays),
                    ActualCost = courseVM.ActualCost,
                    TargetSector = MethodsCheck.chechName(courseVM.TargetSector),
                    Cost = courseVM.Cost,
                    CourseNatureId = courseVM.CourseNatureId,
                    TrainingSpecialistId = courseVM.TrainingSpecialistId,
                    HoursNumber = courseVM.HoursNumber,
                    Material = courseVM.Material,
                    Notes = MethodsCheck.chechName(courseVM.Notes),
                    RoomNumber = courseVM.RoomNumber,
                    ImplementationMonth = courseVM.ImplementationMonth,
                    ImplementationPlace = MethodsCheck.chechName(courseVM.ImplementationPlace),
                    ImplementationTypeId = courseVM.ImplementationTypeId,
                    Rating = courseVM.Rating,
                    TotalImplementationId = courseVM.TotalImplementationId,
                    TraineesNumber = courseVM.TraineesNumber,
                    BeginningDate = courseVM.BeginningDate,
                    EndingDate = courseVM.EndingDate,
                    Participants = MethodsCheck.chechName(courseVM.Participants),
                    Link = courseVM.Link,
                    RatingSpecialist = courseVM.RatingSpecialist,
                    RatingSpecialistNotes = courseVM.RatingSpecialistNotes,
                    TraineesNotes = courseVM.RatingSpecialistNotes,
                    TraineesRating = courseVM.TraineesRating,
                    ImplementedCenter = courseVM.ImplementedCenter,
                    FundingEntity = courseVM.FundingEntity,
                };


                courseRepository.Create(course);
                courseRepository.Commit();


                if (file != null)
                {
                    string fileName = $"pdf{course.Id}".ToString();
                    string extension = Path.GetExtension(file.FileName);


                    fileName += extension;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CouresFiles", fileName);

                    string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CouresFiles");


                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    course.PdfFile = fileName;

                    courseRepository.Edit(course);
                    courseRepository.Commit();
                }


                return RedirectToAction(nameof(Index));
            }
            ViewBag.CourseNature = courseNatureRepository.Get().ToList();
            ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
            ViewBag.TotalImplementation = totalImplementationRepository.Get().ToList();
            ViewBag.ImplementationType = implementationTypeRepository.Get().ToList();
            ViewBag.Material = StaticData.material;
            ViewBag.CourseType = StaticData.courseType;
            ViewBag.ImplementationMonth = StaticData.implementationMonth;
            ViewBag.TrainingSpecialist = trainingSpecialistRepository.Get().ToList();
            ViewBag.Check = StaticData.check;

            return View(courseVM);
        }

        // GET: Manage/Course/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var course = courseRepository.Get(includeProps: [
                c => c.CourseNature!
                ,c => c.TotalImplementation!
                ,c => c.ImplementationType!
                ,c=>c.TrainingSpecialist!]
                , expression: m => m.Id == id).FirstOrDefault(); ;
            if (course == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var courseVM = new CourseVM()
            {
                Id = course.Id,
                Name = course.Name,
                DaysCount = course.DaysCount,
                CourseType = course.CourseType,
                Check = course.Check,
                Code = course.Code,
                ImplementedDays = course.ImplementedDays,
                ActualCost = course.ActualCost,
                TargetSector = course.TargetSector,
                Cost = course.Cost,
                CourseNatureId = course.CourseNatureId,
                TrainingSpecialistId = course.TrainingSpecialistId,
                HoursNumber = course.HoursNumber,
                Material = course.Material,
                Notes = course.Notes,
                RoomNumber = course.RoomNumber,
                ImplementationMonth = course.ImplementationMonth,
                ImplementationPlace = course.ImplementationPlace,
                ImplementationTypeId = course.ImplementationTypeId,
                Rating = course.Rating,
                TotalImplementationId = course.TotalImplementationId,
                TraineesNumber = course.TraineesNumber,
                BeginningDate = course.BeginningDate,
                EndingDate = course.EndingDate,
                PdfFile = course.PdfFile,
                Participants = course.Participants,
                Link = course.Link,
                RatingSpecialist = course.RatingSpecialist,
                TraineesNotes = course.TraineesNotes,
                TraineesRating = course.TraineesRating,
                RatingSpecialistNotes = course.RatingSpecialistNotes,
                ImplementedCenter = course.ImplementedCenter,
                FundingEntity = course.FundingEntity
            };

            ViewBag.CourseNature = courseNatureRepository.Get().ToList();
            ViewBag.TotalImplementation = totalImplementationRepository.Get().ToList();
            ViewBag.ImplementationType = implementationTypeRepository.Get().ToList();
            ViewBag.Material = StaticData.material;
            ViewBag.CourseType = StaticData.courseType;
            ViewBag.ImplementationMonth = StaticData.implementationMonth;
            ViewBag.TrainingSpecialist = trainingSpecialistRepository.Get().ToList();
            ViewBag.Check = StaticData.check;


            return View(courseVM);
        }

        // POST: Manage/Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CourseVM courseVM, IFormFile file)
        {
            if (id != courseVM.Id)
            {
                return RedirectToAction("Notfound", "Home");
            }
            if (!CourseExists(courseVM.Id))
            {
                return RedirectToAction("Notfound", "Home");
            }
            ModelState.Remove(nameof(file));
            if (ModelState.IsValid)
            {
                if (courseVM.BeginningDate! > courseVM.EndingDate)
                {
                    ModelState.AddModelError(string.Empty, "تاريخ البداية لا يمكن ان يكون بعد تاريخ النهاية");

                    ViewBag.CourseNature = courseNatureRepository.Get().ToList();
                    ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
                    ViewBag.TotalImplementation = totalImplementationRepository.Get().ToList();
                    ViewBag.ImplementationType = implementationTypeRepository.Get().ToList();
                    ViewBag.Material = StaticData.material;
                    ViewBag.CourseType = StaticData.courseType;
                    ViewBag.ImplementationMonth = StaticData.implementationMonth;
                    ViewBag.TrainingSpecialist = trainingSpecialistRepository.Get().ToList();
                    ViewBag.Check = StaticData.check;

                    return View();
                }

                if (file != null)
                {
                    if (Path.GetExtension(file.FileName).ToLower() != ".pdf")
                    {
                        ModelState.AddModelError(nameof(courseVM.PdfFile), "يسمح برفع ملفات pdf فقط");

                        ViewBag.CourseNature = courseNatureRepository.Get().ToList();
                        ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
                        ViewBag.TotalImplementation = totalImplementationRepository.Get().ToList();
                        ViewBag.ImplementationType = implementationTypeRepository.Get().ToList();
                        ViewBag.Material = StaticData.material;
                        ViewBag.CourseType = StaticData.courseType;
                        ViewBag.ImplementationMonth = StaticData.implementationMonth;
                        ViewBag.TrainingSpecialist = trainingSpecialistRepository.Get().ToList();
                        ViewBag.Check = StaticData.check;

                        return View(courseVM);
                    }
                }

                var course = new Course()
                {
                    Id = courseVM.Id,
                    Name = MethodsCheck.chechName(courseVM.Name),
                    DaysCount = courseVM.DaysCount,
                    CourseType = courseVM.CourseType,
                    Check = courseVM.Check,
                    Code = MethodsCheck.chechName(courseVM.Code),
                    ImplementedDays = MethodsCheck.chechName(courseVM.ImplementedDays),
                    ActualCost = courseVM.ActualCost,
                    TargetSector = MethodsCheck.chechName(courseVM.TargetSector),
                    Cost = courseVM.Cost,
                    CourseNatureId = courseVM.CourseNatureId,
                    TrainingSpecialistId = courseVM.TrainingSpecialistId,
                    HoursNumber = courseVM.HoursNumber,
                    Material = courseVM.Material,
                    Notes = MethodsCheck.chechName(courseVM.Notes),
                    RoomNumber = courseVM.RoomNumber,
                    ImplementationMonth = courseVM.ImplementationMonth,
                    ImplementationPlace = MethodsCheck.chechName(courseVM.ImplementationPlace),
                    ImplementationTypeId = courseVM.ImplementationTypeId,
                    Rating = courseVM.Rating,
                    TotalImplementationId = courseVM.TotalImplementationId,
                    TraineesNumber = courseVM.TraineesNumber,
                    BeginningDate = courseVM.BeginningDate,
                    EndingDate = courseVM.EndingDate,
                    Participants = MethodsCheck.chechName(courseVM.Participants),
                    Link = courseVM.Link,
                    RatingSpecialist = courseVM.RatingSpecialist,
                    RatingSpecialistNotes = courseVM.RatingSpecialistNotes,
                    TraineesNotes = courseVM.RatingSpecialistNotes,
                    TraineesRating = courseVM.TraineesRating,
                    ImplementedCenter = courseVM.ImplementedCenter,
                    PdfFile = courseVM.PdfFile,
                    FundingEntity = courseVM.FundingEntity,

                };
                courseRepository.Edit(course);
                courseRepository.Commit();

                if (file != null)
                {
                    string fileName = $"pdf{course.Id}".ToString();
                    string extension = Path.GetExtension(file.FileName);


                    fileName += extension;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CouresFiles", fileName);

                    string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CouresFiles");


                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    course.PdfFile = fileName;

                    courseRepository.Edit(course);
                    courseRepository.Commit();
                }

                var courseInstructor = courseInstructorRepository.Get(expression: e => e.CourseId == course.Id, tracked: false).ToList();

                foreach (var item in courseInstructor)
                {
                    courseInstructorRepository.Delete(item);
                    courseInstructorRepository.Commit();

                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CourseNature = courseNatureRepository.Get().ToList();
            ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
            ViewBag.TotalImplementation = totalImplementationRepository.Get().ToList();
            ViewBag.ImplementationType = implementationTypeRepository.Get().ToList();
            ViewBag.Material = StaticData.material;
            ViewBag.CourseType = StaticData.courseType;
            ViewBag.ImplementationMonth = StaticData.implementationMonth;
            ViewBag.TrainingSpecialist = trainingSpecialistRepository.Get().ToList();
            ViewBag.Check = StaticData.check;

            return View(courseVM);
        }



        // POST: Manage/Course/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var course = courseRepository.Get(expression: m => m.Id == id).FirstOrDefault();
            if (course != null)
            {
                if (course.PdfFile != null)
                {

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CouresFiles", course.PdfFile);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
                string courseFilesPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\TraineesFiles\\Course{course.Id}");
                if (Directory.Exists(courseFilesPath))
                {
                    Directory.Delete(courseFilesPath, true);
                }

                courseRepository.Delete(course);

                courseRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Notfound", "Home");

        }
        public IActionResult DownloadFile(string fileName)
        {
            if (fileName != null)
            {

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\CouresFiles", fileName);
                if (System.IO.File.Exists(filePath))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                    return File(fileBytes, "application/pdf", fileName);
                }
            }
            TempData["noFile"] = "there is no file to download";
            return View();

        }
        public IActionResult print(string? Name, DateOnly? BeginningDate, DateOnly? EndingDate, string? ImplementationPlace
            , string? ImplementedCenter, int? ImplementationTypeId, int? TotalImplementationId, Check? Check, int? CourseNatureId
            , string? Instructor, int Type,int page=1, bool Sort = false)
        {
            IQueryable<Course> qcourses = courseRepository.Get(includeProps: [
               e=>e.CourseNature!, e=>e.TotalImplementation! ,e=>e.ImplementationType! ,e=>e.TrainingSpecialist!
               ]);
            qcourses = qcourses.Include(e => e.CoursesInstructors).ThenInclude(e => e.Instructor);

            if (Type == 1 || Type == 2)
            {
                if (BeginningDate == null || EndingDate == null)
                {
                    TempData["Date"] = "يجب ادخال تاريخ بداية و تاريخ نهاية";

                    return RedirectToAction("index");
                }
                string path;
                if (Type == 1)
                {
                    qcourses = qcourses.Where(e => e.Check == Models.EnumClasses.Check.Pending && e.BeginningDate >= BeginningDate
                    && e.EndingDate <= EndingDate);
                       if(CourseNatureId != null)
                    {
                        qcourses = qcourses.Where(e => e.CourseNature!.Id == CourseNatureId);
                    }
                    qcourses =qcourses.OrderBy(e => e.BeginningDate).ThenBy(e => e.EndingDate);

                    var courses = qcourses.Select(e => new
                    {
                        e.Name,
                        e.TargetSector,
                        e.Participants,
                        e.ImplementationPlace,
                        e.DaysCount,
                        BeginningDate = e.BeginningDate.ToString(),
                        EndingDate = e.EndingDate.ToString(),
                        e.TraineesNumber,
                        e.Cost,
                        TotalImplementationName = e.TotalImplementation!.Name,
                        CourseNatureName = e.CourseNature!.Name,
                        e.FundingEntity,
                        FirstInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.First).Select(e => e.Instructor!.Name).FirstOrDefault(),
                        SecondInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Second).Select(e => e.Instructor!.Name).FirstOrDefault(),
                        ThirdInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Third).Select(e => e.Instructor!.Name).FirstOrDefault(),
                        ForthInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Fourth).Select(e => e.Instructor!.Name).FirstOrDefault()

                    });
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Reports\\CoursesF78.rdlc");


                    var CourseNature = courseNatureRepository.Get(expression: e => e.Id == CourseNatureId).Select(e => e.Name).FirstOrDefault();

                    var report = new LocalReport();
                    report.ReportPath = path;

                    report.DataSources.Add(new ReportDataSource("Courses", courses));

                    var parameters = new[]
                    {
                   new ReportParameter("CourseNatureName", CourseNature),
                };
                    report.SetParameters(parameters);

                    byte[] pdf = report.Render("PDF");

                    return File(pdf, "application/pdf");
                }
                else
                {
                  
                    qcourses = qcourses.Where(e => e.Check == Models.EnumClasses.Check.Completed && e.BeginningDate >= BeginningDate
                    && e.EndingDate <= EndingDate);
                    if (CourseNatureId != null)
                    {
                        qcourses = qcourses.Where(e => e.CourseNature!.Id == CourseNatureId);
                    }
                    qcourses = qcourses.OrderBy(e => e.BeginningDate).ThenBy(e => e.EndingDate);


                    var courses = (List<Course>)qcourses.Select(e => new
                    {
                        e.Name,
                        e.TargetSector,
                        e.Participants,
                        e.ImplementationPlace,
                        e.DaysCount,
                        BeginningDate = e.BeginningDate.ToString(),
                        EndingDate = e.EndingDate.ToString(),
                        e.TraineesNumber,
                        e.Cost,
                        TotalImplementationName = e.TotalImplementation!.Name,
                        CourseNatureName = e.CourseNature!.Name,
                        TrainingSpecialistName = e.TrainingSpecialist!.Name,
                        e.FundingEntity,
                        FirstInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.First).Select(e => e.Instructor!.Name).FirstOrDefault(),
                        SecondInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Second).Select(e => e.Instructor!.Name).FirstOrDefault(),
                        ThirdInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Third).Select(e => e.Instructor!.Name).FirstOrDefault(),
                        ForthInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Fourth).Select(e => e.Instructor!.Name).FirstOrDefault()

                    });
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Reports\\CoursesF78.rdlc");


                    var CourseNature = courseNatureRepository.Get(expression: e => e.Id == CourseNatureId).Select(e => e.Name).FirstOrDefault();

                    var report = new LocalReport();
                    report.ReportPath = path;

                    report.DataSources.Add(new ReportDataSource("Courses", courses));

                    var parameters = new[]
                    {
                   new ReportParameter("CourseNatureName", CourseNature),
                };
                    report.SetParameters(parameters);

                    byte[] pdf = report.Render("PDF");

                    return File(pdf, "application/pdf");
                }

            }
            else
            {
                if (Name != null)
                {
                    qcourses = qcourses.Where(e => e.Name!.Contains(Name.TrimStart().TrimEnd()));
                }
                if (BeginningDate != null)
                {
                    qcourses = qcourses.Where(e => e.BeginningDate >= BeginningDate);
                }
                if (EndingDate != null)
                {
                    qcourses = qcourses.Where(e => e.EndingDate <= EndingDate);
                }
                if (ImplementationPlace != null)
                {
                    qcourses = qcourses.Where(e => e.ImplementationPlace!.Contains(ImplementationPlace.TrimStart().TrimEnd()));
                }
                if (ImplementedCenter != null)
                {
                    qcourses = qcourses.Where(e => e.ImplementedCenter!.Contains(ImplementedCenter.TrimStart().TrimEnd()));
                }
                if (ImplementationTypeId != null)
                {
                    qcourses = qcourses.Where(e => e.ImplementationType!.Id == ImplementationTypeId);
                }
                if (TotalImplementationId != null)
                {
                    qcourses = qcourses.Where(e => e.TotalImplementation!.Id == TotalImplementationId);
                }
                if (Check != null)
                {
                    qcourses = qcourses.Where(e => e.Check == Check);
                }
                if (CourseNatureId != null)
                {
                    qcourses = qcourses.Where(e => e.CourseNature!.Id == CourseNatureId);
                }
                if (Instructor != null)
                {
                    qcourses = qcourses.Where(e => e.CoursesInstructors.Any(c => c.Instructor!.Name!.Contains(Instructor.TrimStart().TrimEnd())));
                }
                if (!Sort)
                    qcourses = qcourses.OrderBy(e => e.BeginningDate).ThenBy(e => e.EndingDate);
                else
                    qcourses = qcourses.OrderByDescending(e => e.BeginningDate).ThenByDescending(e => e.EndingDate);

                var courses = qcourses.Select(e => new
                {
                    //Id = e.Id,
                    Name = e.Name,
                    TargetSector = e.TargetSector,
                    Participants = e.Participants,
                    ImplementationPlace = e.ImplementationPlace,
                    DaysCount = e.DaysCount,
                    ImplementedDays = e.ImplementedDays,
                    BeginningDate = e.BeginningDate.ToString(),
                    EndingDate = e.EndingDate.ToString(),
                    TraineesNumber = e.TraineesNumber,
                    Cost = e.Cost,
                    ImplementedCenter = e.ImplementedCenter,
                    HoursNumber = e.HoursNumber,
                    ImplementationTypeName = e.ImplementationType!.Name,
                    TotalImplementationName = e.TotalImplementation!.Name,
                    RoomNumber = e.RoomNumber,
                    Material = e.Material,
                    CourseType = e.CourseType,
                    Rating = e.Rating,
                    ImplementationMonth = e.ImplementationMonth,
                    ActualCost = e.ActualCost,
                    Code = e.Code,
                    Check = e.Check,
                    PdfFile = e.PdfFile,
                    EnterName = e.EnterName,
                    Link = e.Link,
                    RatingSpecialist = e.RatingSpecialist,
                    Notes = e.Notes,
                    RatingSpecialistNotes = e.RatingSpecialistNotes,
                    TraineesNotes = e.TraineesNotes,
                    TraineesRating = e.TraineesRating,
                    CourseNatureName = e.CourseNature!.Name,
                    TrainingSpecialistName = e.TrainingSpecialist!.Name,
                    FundingEntity = e.FundingEntity,
                    FirstInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.First).Select(e => e.Instructor!.Name).FirstOrDefault(),
                    SecondInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Second).Select(e => e.Instructor!.Name).FirstOrDefault(),
                    ThirdInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Third).Select(e => e.Instructor!.Name).FirstOrDefault(),
                    ForthInstructorName = e.CoursesInstructors.Where(c => c.Position == Position.Fourth).Select(e => e.Instructor!.Name).FirstOrDefault()

                });

            }







            //  var TotalImplementation = totalImplementationRepository.Get(expression: e => e.Id == TotalImplementationId).Select(e => e.Name).FirstOrDefault();




            // var report = new LocalReport();
            //    report.ReportPath = path;
            //  // Add the data source
            //  report.DataSources.Add(new ReportDataSource("Courses", courses));

            var time = DateTime.Now.Month;
            ////   Set parameters
            //var parameters = new[]
            // {
            //       new ReportParameter("CourseNatureName", CourseNature),
            //  //  new ReportParameter("TotalImplementationName", $"({TotalImplementation})"),
            //       new ReportParameter("Month",StaticData.implementationMonth.GetValueOrDefault( (ImplementationMonth)(time-1) ) )
            //   };
            //report.SetParameters(parameters);

            ////   // Render the report
            //byte[] pdf = report.Render("PDF");

            //return File(pdf, "application/pdf");

            //  // Render the report as an Excel file
            //  byte[] excel = report.Render("EXCELOPENXML");

            //  return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
            return View("index");

        }
        private bool CourseExists(int id)
        {
            return courseRepository.Get(tracked: false).Any(e => e.Id == id);
        }
    }
}
