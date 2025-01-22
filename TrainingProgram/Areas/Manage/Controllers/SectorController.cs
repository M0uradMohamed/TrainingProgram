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
using System.Xml.Linq;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SectorController : Controller
    {
        private readonly ISectorRepository sectorRepository;

        public SectorController(ISectorRepository sectorRepository)
        {
            this.sectorRepository = sectorRepository;
        }

        // GET: Manage/Sector
        public IActionResult Index()
        {
            return View(sectorRepository.Get().ToList());
        }

        // GET: Manage/Sector/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var sector = sectorRepository.Get(expression: m => m.Id == id)
                .FirstOrDefault();
            if (sector == null)
            {
                return RedirectToAction ("Notfound", "Home");
            }
     

            return View(sector);
        }

        // GET: Manage/Sector/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Manage/Sector/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SectorVM sectorVM)
        {
            if (ModelState.IsValid)
            {
                var sector = new Sector()
                {
                    Id = sectorVM.Id,
                    Name = sectorVM.Name
                };

                sectorRepository.Create(sector);
               sectorRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(sectorVM);
        }

        // GET: Manage/Sector/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var sector = sectorRepository.Get(expression:e=>e.Id== id).FirstOrDefault();
            if (sector == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
            var sectorVM = new SectorVM()
            {
                Id = sector.Id,
                Name = sector.Name
            };


            return View(sectorVM);
        }

        // POST: Manage/Sector/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult  Edit(int id, SectorVM sectorVM)
        {
            if (id != sectorVM.Id)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var sector = new Sector()
                    {
                        Id = sectorVM.Id,
                        Name = sectorVM.Name
                    };

                    sectorRepository.Edit(sector);
                    sectorRepository.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectorExists(sectorVM.Id))
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
            return View(sectorVM);
        }

    

        // POST: Manage/Sector/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var sector = sectorRepository.Get(expression: m => m.Id == id).FirstOrDefault();
            if (sector != null)
            {
                sectorRepository.Delete(sector);
            }

            sectorRepository.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool SectorExists(int id)
        {
            return sectorRepository.Get().Any(e => e.Id == id);
        }
    }
}
