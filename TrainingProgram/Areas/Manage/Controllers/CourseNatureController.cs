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
using Models.ViewModels;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CourseNatureController : Controller
    {
        private readonly ICourseNatureRepository courseNatureRepository;

        public CourseNatureController(ICourseNatureRepository courseNatureRepository)
        {
            this.courseNatureRepository = courseNatureRepository;
        }

        // GET: Manage/CourseNature
        public IActionResult Index()
        {
            return View(courseNatureRepository.Get().ToList());
        }

        // GET: Manage/CourseNature/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var courseNature = courseNatureRepository.Get(expression: m => m.Id == id)
                .FirstOrDefault();
            if (courseNature == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
 


            return View(courseNature);
        }

        // GET: Manage/CourseNature/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manage/CourseNature/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseNatureVM courseNatureVM)
        {
            if (ModelState.IsValid)
            {
                var courseNature = new CourseNature()
                {
                    Id = courseNatureVM.Id,
                    Name = courseNatureVM.Name
                };

                courseNatureRepository.Create(courseNature);
                courseNatureRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(courseNatureVM);
        }

        // GET: Manage/CourseNature/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var courseNature = courseNatureRepository.Get(expression: e => e.Id == id).FirstOrDefault();
            if (courseNature == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
            var courseNatureVM = new CourseNatureVM()
            {
                Id = courseNature.Id,
                Name = courseNature.Name
            };


            return View(courseNatureVM);
        }

        // POST: Manage/CourseNature/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, CourseNatureVM courseNatureVM)
        {
            if (id != courseNatureVM.Id)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var courseNature = new CourseNature()
                    {
                        Id = courseNatureVM.Id,
                        Name = courseNatureVM.Name
                    };

                    courseNatureRepository.Edit(courseNature);
                    courseNatureRepository.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseNatureExists(courseNatureVM.Id))
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
            return View(courseNatureVM);
        }



        // POST: Manage/CourseNature/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var courseNature = courseNatureRepository.Get(expression: m => m.Id == id).FirstOrDefault();
            if (courseNature != null)
            {
                courseNatureRepository.Delete(courseNature);
            }

            courseNatureRepository.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseNatureExists(int id)
        {
            return courseNatureRepository.Get().Any(e => e.Id == id);
        }
    }
}
