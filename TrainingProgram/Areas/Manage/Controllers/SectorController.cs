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
using Microsoft.AspNetCore.Hosting;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SectorController : Controller
    {
        private readonly ISectorRepository sectorRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SectorController(ISectorRepository sectorRepository , IWebHostEnvironment webHostEnvironment)
        {
            this.sectorRepository = sectorRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Manage/Sector
        public IActionResult Index(int page =1)
        {
            var sectors = sectorRepository.Get();
                           double totalPages = Math.Ceiling((double)sectors.Count() / 5);
            sectors = sectors.Skip((page - 1) * 5).Take(5);

            ViewBag.Pages = new { page, totalPages };
            return View(sectors.ToList());
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
       /* public IActionResult print()
        {

            // string path = Path.Combine(webHostEnvironment.WebRootPath + @"\Reports\SectorReport.rdlc");
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Reports\\SectorReport.rdlc");


            Dictionary<string, string> parmaeters = new Dictionary<string, string>();
            parmaeters.Add("Ahmed", "شركة المياة");

            var sector = sectorRepository.Get();

            LocalReport localreport = new LocalReport(path);
           localreport.AddDataSource("SectorDataSet", sector);
            var report = localreport.Execute(RenderType.Pdf, 1, null, "");




            return File(report.MainStream, "application/pdf");
        }*/
      /*  public IActionResult Testprint()
        {

             string path = Path.Combine(webHostEnvironment.WebRootPath + @"\Reports\TestRP.rdlc");
           // string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Reports\\SectorReport.rdlc");

            Dictionary<string, string> parmaeters = new Dictionary<string, string>();
            parmaeters.Add("", "شركة المياة");

            var sector = sectorRepository.Get();

            LocalReport localreport = new LocalReport(path);
            localreport.AddDataSource("SectorDataSet", sector);
            var report = localreport.Execute(RenderType.Excel, 1, null, "");


            return File(report.MainStream, "application/vnd.ms-excel");
        }*/
        private bool SectorExists(int id)
        {
            return sectorRepository.Get().Any(e => e.Id == id);
        }
    }
}
