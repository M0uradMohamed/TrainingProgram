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
using Models.ViewModels;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TotalImplementationController : Controller
    {
        private readonly ITotalImplementationRepository totalImplementationRepository;

        public TotalImplementationController(ITotalImplementationRepository totalImplementationRepository)
        {
            this.totalImplementationRepository = totalImplementationRepository;
        }

        // GET: Manage/TotalImplementation
        public IActionResult Index(int page =1)
        {
          var totalImplementations  = totalImplementationRepository.Get();
            double totalPages = Math.Ceiling((double)totalImplementations.Count() / 5);
            totalImplementations = totalImplementations.Skip((page - 1) * 5).Take(5);

            ViewBag.Pages = new { page, totalPages };
            return View(totalImplementations.ToList());
        }

        // GET: Manage/TotalImplementation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var totalImplementation = totalImplementationRepository.Get(expression: m => m.Id == id)
                .FirstOrDefault();
            if (totalImplementation == null)
            {
                return RedirectToAction("Notfound", "Home");
            }


            return View(totalImplementation);
        }

        // GET: Manage/TotalImplementation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manage/TotalImplementation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TotalImplementationVM totalImplementationVM)
        {
            if (ModelState.IsValid)
            {
                var totalImplementation = new TotalImplementation()
                {
                    Id = totalImplementationVM.Id,
                    Name = totalImplementationVM.Name
                };

                totalImplementationRepository.Create(totalImplementation);
                totalImplementationRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(totalImplementationVM);
        }

        // GET: Manage/TotalImplementation/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var totalImplementation = totalImplementationRepository.Get(expression: e => e.Id == id).FirstOrDefault();
            if (totalImplementation == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
            var totalImplementationVM = new TotalImplementationVM()
            {
                Id = totalImplementation.Id,
                Name = totalImplementation.Name
            };


            return View(totalImplementationVM);
        }

        // POST: Manage/TotalImplementation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TotalImplementationVM totalImplementationVM)
        {
            if (id != totalImplementationVM.Id)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var totalImplementation = new TotalImplementation()
                    {
                        Id = totalImplementationVM.Id,
                        Name = totalImplementationVM.Name
                    };

                    totalImplementationRepository.Edit(totalImplementation);
                    totalImplementationRepository.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TotalImplementationExists(totalImplementationVM.Id))
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
            return View(totalImplementationVM);
        }



        // POST: Manage/TotalImplementation/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var totalImplementation = totalImplementationRepository.Get(expression: m => m.Id == id).FirstOrDefault();
            if (totalImplementation != null)
            {
                totalImplementationRepository.Delete(totalImplementation);
            }

            totalImplementationRepository.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool TotalImplementationExists(int id)
        {
            return totalImplementationRepository.Get().Any(e => e.Id == id);
        }
    }
}
