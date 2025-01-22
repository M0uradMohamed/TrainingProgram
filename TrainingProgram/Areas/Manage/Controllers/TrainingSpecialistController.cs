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
    public class TrainingSpecialistController : Controller
    {
        private readonly ITrainingSpecialistRepository trainingSpecialistRepository;

        public TrainingSpecialistController(ITrainingSpecialistRepository trainingSpecialistRepository)
        {
            this.trainingSpecialistRepository = trainingSpecialistRepository;
        }

        // GET: Manage/TrainingSpecialist
        public IActionResult Index()
        {
            return View(trainingSpecialistRepository.Get().ToList());
        }

        // GET: Manage/TrainingSpecialist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var trainingSpecialist = trainingSpecialistRepository.Get(expression: m => m.Id == id)
                .FirstOrDefault();
            if (trainingSpecialist == null)
            {
                return RedirectToAction("Notfound", "Home");
            }


            return View(trainingSpecialist);
        }

        // GET: Manage/TrainingSpecialist/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manage/TrainingSpecialist/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TrainingSpecialistVM trainingSpecialistVM)
        {
            if (ModelState.IsValid)
            {
                var trainingSpecialist = new TrainingSpecialist()
                {
                    Id = trainingSpecialistVM.Id,
                    Name = trainingSpecialistVM.Name
                };

                trainingSpecialistRepository.Create(trainingSpecialist);
                trainingSpecialistRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingSpecialistVM);
        }

        // GET: Manage/TrainingSpecialist/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var trainingSpecialist = trainingSpecialistRepository.Get(expression: e => e.Id == id).FirstOrDefault();
            if (trainingSpecialist == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
            var trainingSpecialistVM = new TrainingSpecialistVM()
            {
                Id = trainingSpecialist.Id,
                Name = trainingSpecialist.Name
            };


            return View(trainingSpecialistVM);
        }

        // POST: Manage/TrainingSpecialist/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TrainingSpecialistVM trainingSpecialistVM)
        {
            if (id != trainingSpecialistVM.Id)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var trainingSpecialist = new TrainingSpecialist()
                    {
                        Id = trainingSpecialistVM.Id,
                        Name = trainingSpecialistVM.Name
                    };

                    trainingSpecialistRepository.Edit(trainingSpecialist);
                    trainingSpecialistRepository.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingSpecialistExists(trainingSpecialistVM.Id))
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
            return View(trainingSpecialistVM);
        }



        // POST: Manage/TrainingSpecialist/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var trainingSpecialist = trainingSpecialistRepository.Get(expression: m => m.Id == id).FirstOrDefault();
            if (trainingSpecialist != null)
            {
                trainingSpecialistRepository.Delete(trainingSpecialist);
            }

            trainingSpecialistRepository.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingSpecialistExists(int id)
        {
            return trainingSpecialistRepository.Get().Any(e => e.Id == id);
        }
    }
}
