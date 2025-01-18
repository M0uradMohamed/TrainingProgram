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
        public async Task<IActionResult> Details(int? id)
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
        public IActionResult Create(Sector sector)
        {
            if (ModelState.IsValid)
            {
                sectorRepository.Create(sector);
               sectorRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(sector);
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
            return View(sector);
        }

        // POST: Manage/Sector/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult  Edit(int id, Sector sector)
        {
            if (id != sector.Id)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sectorRepository.Edit(sector);
                    sectorRepository.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectorExists(sector.Id))
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
            return View(sector);
        }

    

        // POST: Manage/Sector/Delete/5
        [HttpPost]
      //  [ValidateAntiForgeryToken]
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
