using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models;
using DataAccess.Repository.IRepository;
using DataAccess.Repository;
using Models.StaticData;
using Models.ViewModels;
using Models.EnumClasses;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CourseInstructorController : Controller
    {
        private readonly ICourseInstructorRepository courseInstructorRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IInstructorRepository instructorRepository;

        public CourseInstructorController(ICourseInstructorRepository courseInstructorRepository , ICourseRepository courseRepository
            ,IInstructorRepository instructorRepository)
        {
            this.courseInstructorRepository = courseInstructorRepository;
            this.courseRepository = courseRepository;
            this.instructorRepository = instructorRepository;
        }

        // GET: Manage/CourseInstructor
        public  IActionResult Index(int id)
        {
            var IsCourse = courseRepository.Get(expression: e => e.Id == id).Any();

            if (!IsCourse)
            {
                return RedirectToAction("Notfound", "Home");

            }
            var course = courseRepository.Get(expression: e => e.Id == id , includeProps: [e => e.Instructors]).Select(e => new
            {
                e.Id,
                e.Name,
                e.BeginningDate,
                e.EndingDate,
                e.ImplementationPlace
            }).FirstOrDefault();

            ViewBag.Course = course;

            var courseInstructors = courseInstructorRepository.Get(expression:e=>e.CourseId ==id,includeProps:[e=>e.Instructor])
                .OrderBy(e=>e.Position).ToList();
            return View(courseInstructors);
        }

        // GET: Manage/CourseInstructor/Details/5
        public  IActionResult Details(int? id , int instructorId)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var courseInstructor = courseInstructorRepository.Get(expression: e => e.CourseId == id &&
            e.InstructorId == instructorId).FirstOrDefault();
         
            if (courseInstructor == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            return View(courseInstructor);
        }

        // GET: Manage/CourseInstructor/Create
        public IActionResult Create(int id )
        {
            var IsCourse = courseRepository.Get(expression: e => e.Id == id).Any();

            if (!IsCourse)
            {
                return RedirectToAction("Notfound", "Home");

            }
            ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
            ViewBag.CourseId = id;
            ViewBag.Position = StaticData.position;

            return View();
        }

        // POST: Manage/CourseInstructor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id ,  CourseInstructorVM courseInstructorVM)
        {
            if (ModelState.IsValid)
            {
                var IsInstructorIn = courseInstructorRepository.Get(expression: e => e.CourseId == id)
                    .Any(e => e.InstructorId == courseInstructorVM.InstructorId);
                if(IsInstructorIn)
                {
                    ModelState.AddModelError(nameof(courseInstructorVM.InstructorId), "هذا المدرب موجود بالفعل في هذه الدورة");

                    ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
                    ViewBag.CourseId = id;
                    ViewBag.Position = StaticData.position;
                    return View(courseInstructorVM);
                }
                var similerPosition = courseInstructorRepository.Get(expression: e => e.CourseId == id)
                    .Any(e => e.Position == courseInstructorVM.Position);

                if (similerPosition)
                {
                    ModelState.AddModelError(nameof(courseInstructorVM.Position), "هذا الموضع  موجود بالفعل لمدرب");

                    ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
                    ViewBag.CourseId = id;
                    ViewBag.Position = StaticData.position;
                    return View(courseInstructorVM);
                }
                var course = courseRepository.Get(expression: e=>e.Id == id,tracked:false).Select(e=> new
                {
                    e.BeginningDate,
                    e.EndingDate,
                }).FirstOrDefault();

                var similerCourse = courseInstructorRepository.Get(expression: e => e.InstructorId == courseInstructorVM.InstructorId && e.Position == Position.First ,
                    includeProps: [e=>e.Course]).ToList()
               .Any(e => course.BeginningDate <= e.Course.EndingDate &&
                             course.EndingDate >= e.Course.BeginningDate);

                if (similerCourse)
                {
                    ModelState.AddModelError(string.Empty, "لا يمكن تسجيل دورة بهذا المدرب في هذان الموعدان");

                    ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
                    ViewBag.CourseId = id;
                    ViewBag.Position = StaticData.position;
                    return View(courseInstructorVM);
                }

                var courseInstructor = new CourseInstructor()
                {
                   CourseId = id,
                   InstructorId=courseInstructorVM.InstructorId,
                   CourseNotes = courseInstructorVM.CourseNotes,
                   Position = courseInstructorVM.Position,
                   Rating = courseInstructorVM.Rating
                    
                };
                courseInstructorRepository.Create(courseInstructor);
                courseInstructorRepository.Commit();

                TempData["success"] = $"  تم اضافة المدرب بنجاح ,";
                ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
                ViewBag.CourseId = id;
                ViewBag.Position = StaticData.position;
                ModelState.Clear();
                return View();
            }

            ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
            ViewBag.CourseId = id;
            ViewBag.Position = StaticData.position;
            return View(courseInstructorVM);
        }

        // GET: Manage/CourseInstructor/Edit/5
        public  IActionResult Edit(int? id, int instructorId)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
            var IsCourse = courseRepository.Get(expression: e => e.Id == id).Any();

            if (!IsCourse)
            {
                return RedirectToAction("Notfound", "Home");

            }
            var courseInstructor = courseInstructorRepository.Get(expression: e =>e.CourseId  == id && 
            e.InstructorId == instructorId).FirstOrDefault();

            if (courseInstructor == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
            var courseInstructorVM = new CourseInstructorVM()
            {
                CourseId=courseInstructor.CourseId,
                InstructorId = courseInstructor.InstructorId,
                CourseNotes = courseInstructor.CourseNotes,
                Rating = courseInstructor.Rating,
                Position = courseInstructor.Position
            };
            var instructor = courseInstructorRepository.Get(expression: e => e.CourseId == id &&
            e.InstructorId == instructorId, tracked:false). Select(e=> new
            {
                e.Instructor.Name, e.Position
            }).FirstOrDefault();

            ViewBag.Instructor = instructor;
            ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
            ViewBag.CourseId = id;
            ViewBag.Position = StaticData.position;

            return View(courseInstructorVM);
        }

        // POST: Manage/CourseInstructor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CourseInstructorVM courseInstructorVM)
        {
            if (id != courseInstructorVM.CourseId)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {
                

                var courseinstructor = new CourseInstructor()
                {
                    InstructorId=courseInstructorVM.InstructorId,
                    CourseId=id,
                    CourseNotes=courseInstructorVM.CourseNotes,
                    Rating=courseInstructorVM.Rating,
                    Position=courseInstructorVM.Position
                };
                courseInstructorRepository.Edit(courseinstructor);
                courseInstructorRepository.Commit();

                return RedirectToAction(nameof(Index), new { id = $"{id}" });
            }
            return View(courseInstructorVM);
        }



        // POST: Manage/CourseInstructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, int instructorId)
        {
            var courseInstructor = courseInstructorRepository.Get(expression: e => e.CourseId == id &&
             e.InstructorId == instructorId).FirstOrDefault();
            if (courseInstructor != null)
            {
                courseInstructorRepository.Delete(courseInstructor);
                courseInstructorRepository.Commit();
            }

            return RedirectToAction(nameof(Index), new { id = $"{id}" });
        }

        private bool CourseInstructorExists(int id)
        {
            return courseInstructorRepository.Get().Any(e => e.CourseId == id);
        }
    }
}
