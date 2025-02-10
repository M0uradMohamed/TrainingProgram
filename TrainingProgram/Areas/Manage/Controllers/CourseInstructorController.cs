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

            var courseInstructors = courseInstructorRepository.Get(expression:e=>e.CourseId ==id).ToList();
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

                TempData["success"] = $"  تم اضافة المدرب بنجاح ,";
                ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
                ViewBag.CourseId = id;
                ViewBag.Position = StaticData.position;
                ModelState.Clear();
                return View();
            }
            //   ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseInstructor.CourseId);
            // ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "Id", courseInstructor.InstructorId);
        
            ViewBag.Instructors = instructorRepository.Get().Select(e => new { e.Id, e.FoundationId, e.Name });
            ViewBag.CourseId = id;
            ViewBag.Position = StaticData.position;
            return View(courseInstructorVM);
        }

        // GET: Manage/CourseInstructor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

          //  var courseInstructor = await _context.CoursesInstructors.FindAsync(id);
          /*  if (courseInstructor == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseInstructor.CourseId);
            ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "Id", courseInstructor.InstructorId);
          */
            return View(/*courseInstructor*/);
        }

        // POST: Manage/CourseInstructor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,InstructorId,CourseNotes,Rating")] CourseInstructor courseInstructor)
        {
            if (id != courseInstructor.CourseId)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                   // _context.Update(courseInstructor);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseInstructorExists(courseInstructor.CourseId))
                    {
                        return RedirectToAction("Notfound", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
         //   ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", courseInstructor.CourseId);
          //  ViewData["InstructorId"] = new SelectList(_context.Instructors, "Id", "Id", courseInstructor.InstructorId);
            return View(courseInstructor);
        }

        // GET: Manage/CourseInstructor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

         /*   var courseInstructor = await _context.CoursesInstructors
                .Include(c => c.Course)
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (courseInstructor == null)
            {
                return RedirectToAction("Notfound", "Home");
            }*/

            return View(/*courseInstructor*/);
        }

        // POST: Manage/CourseInstructor/Delete/5
      /*  [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseInstructor = await _context.CoursesInstructors.FindAsync(id);
            if (courseInstructor != null)
            {
                //_context.CoursesInstructors.Remove(courseInstructor);
            }

          //  await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        private bool CourseInstructorExists(int id)
        {
            return courseInstructorRepository.Get().Any(e => e.CourseId == id);
        }
    }
}
