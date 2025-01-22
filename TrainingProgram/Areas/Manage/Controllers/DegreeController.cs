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
    public class DegreeController : Controller
    {
        private readonly IDegreeRepository degreeRepository;

        public DegreeController( IDegreeRepository degreeRepository)
        {
            this.degreeRepository = degreeRepository;
        }

        // GET: Manage/Degree
        public IActionResult Index()
        {
            return View(degreeRepository.Get().ToList());
        }

        // GET: Manage/Degree/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var degree = degreeRepository.Get(expression: m => m.Id == id)
                .FirstOrDefault();
            if (degree == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            return View(degree);
        }

        // GET: Manage/Degree/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manage/Degree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DegreeVM degreeVM)
        {
            if (ModelState.IsValid)
            {
                var degree = new Degree()
                {
                    Id = degreeVM.Id,
                    Name = degreeVM.Name
                };

                degreeRepository.Create(degree);
                degreeRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(degreeVM);
        }

        // GET: Manage/Degree/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var degree = degreeRepository.Get(expression: e => e.Id == id).FirstOrDefault();
            if (degree == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
            var degreeVM = new DegreeVM()
            {
                Id = degree.Id,
                Name = degree.Name
            };


            return View(degreeVM);
        }

        // POST: Manage/Degree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DegreeVM degreeVM)
        {
            if (id != degreeVM.Id)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var degree = new Degree()
                    {
                        Id = degreeVM.Id,
                        Name = degreeVM.Name
                    };

                    degreeRepository.Edit(degree);
                    degreeRepository.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DegreeExists(degreeVM.Id))
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
            return View(degreeVM);
        }

        // POST: Manage/Degree/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var degree = degreeRepository.Get(expression: m => m.Id == id).FirstOrDefault();
            if (degree != null)
            {
                degreeRepository.Delete(degree);
            }

            degreeRepository.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeExists(int id)
        {
            return degreeRepository.Get().Any(e => e.Id == id);
        }
    }
}
