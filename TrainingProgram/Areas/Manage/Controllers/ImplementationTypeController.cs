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
    public class ImplementationTypeController : Controller
    {
        private readonly IImplementationTypeRepository implementationTypeRepository;

        public ImplementationTypeController(IImplementationTypeRepository implementationTypeRepository)
        {
            this.implementationTypeRepository = implementationTypeRepository;
        }

        // GET: Manage/ImplementationType
        public IActionResult Index()
        {
            return View(implementationTypeRepository.Get().ToList());
        }

        // GET: Manage/ImplementationType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var implementationType = implementationTypeRepository.Get(expression: m => m.Id == id)
                .FirstOrDefault();
            if (implementationType == null)
            {
                return RedirectToAction("Notfound", "Home");
            }


            return View(implementationType);
        }

        // GET: Manage/ImplementationType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manage/ImplementationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ImplementationTypeVM implementationTypeVM)
        {
            if (ModelState.IsValid)
            {
                var implementationType = new ImplementationType()
                {
                    Id = implementationTypeVM.Id,
                    Name = implementationTypeVM.Name
                };

                implementationTypeRepository.Create(implementationType);
                implementationTypeRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(implementationTypeVM);
        }

        // GET: Manage/ImplementationType/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var implementationType = implementationTypeRepository.Get(expression: e => e.Id == id).FirstOrDefault();
            if (implementationType == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
            var implementationTypeVM = new ImplementationTypeVM()
            {
                Id = implementationType.Id,
                Name = implementationType.Name
            };


            return View(implementationTypeVM);
        }

        // POST: Manage/ImplementationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ImplementationTypeVM implementationTypeVM)
        {
            if (id != implementationTypeVM.Id)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var implementationType = new ImplementationType()
                    {
                        Id = implementationTypeVM.Id,
                        Name = implementationTypeVM.Name
                    };

                    implementationTypeRepository.Edit(implementationType);
                    implementationTypeRepository.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImplementationTypeExists(implementationTypeVM.Id))
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
            return View(implementationTypeVM);
        }



        // POST: Manage/ImplementationType/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var implementationType = implementationTypeRepository.Get(expression: m => m.Id == id).FirstOrDefault();
            if (implementationType != null)
            {
                implementationTypeRepository.Delete(implementationType);
            }

            implementationTypeRepository.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool ImplementationTypeExists(int id)
        {
            return implementationTypeRepository.Get().Any(e => e.Id == id);
        }
    }
}
