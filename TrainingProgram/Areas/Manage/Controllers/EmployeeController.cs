
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
using Models.StaticData;
using Models.ViewModels;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IDegreeRepository degreeRepository;
        private readonly ISectorRepository sectorRepository;

        public EmployeeController(ApplicationDbContext context , IEmployeeRepository employeeRepository ,
            IDegreeRepository degreeRepository , ISectorRepository sectorRepository) 
        {
            _context = context;
            this.employeeRepository = employeeRepository;
            this.degreeRepository = degreeRepository;
            this.sectorRepository = sectorRepository;
        }

        // GET: Manage/Employee
        public IActionResult Index()
        {
            var employees = employeeRepository.Get(includeProps: [e => e.Degree, e => e.Sector]).ToList();
            return View(employees);
        }

        // GET: Manage/Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = employeeRepository.Get(includeProps: 
               [ e => e.Degree,
                e => e.Sector]
                ,expression: m => m.Id == id)
                .FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Manage/Employee/Create
        public IActionResult Create()
        {
            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();
            ViewBag.Gender = GenderData.gender;
            ViewBag.Belong = BelongData.belong ;
            return View();
        }

        // POST: Manage/Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeVM employeeVM)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    FoundationId = MethodsCheck.chechName(employeeVM.FoundationId),
                    Name= MethodsCheck.chechName(employeeVM.Name),
                    Gender = employeeVM.Gender,
                    Major = MethodsCheck.chechName(employeeVM.Major),
                    Job = MethodsCheck.chechName(employeeVM.Job),
                    Department = MethodsCheck.chechName(employeeVM.Department),
                    SectorId = employeeVM.SectorId,
                    DegreeId = employeeVM.DegreeId,
                    WorkPlace = MethodsCheck.chechName(employeeVM.WorkPlace),
                    PhoneNumber = employeeVM.PhoneNumber,
                    Belong = employeeVM.Belong,
                    CompanyNameForForeign = MethodsCheck.chechName(employeeVM.CompanyNameForForeign)

                };

                employeeRepository.Create(employee);
                 employeeRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();
            ViewBag.Gender = GenderData.gender;
            ViewBag.Belong = BelongData.belong;
            return View(employeeVM);
        }

        // GET: Manage/Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();
            ViewBag.Gender = GenderData.gender;
            ViewBag.Belong = BelongData.belong;
            return View(employee);
        }

        // POST: Manage/Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoundationId,Name,Gender,Major,Job,Department,SectorId,DegreeId,WorkPlace,PhoneNumber,Belong,CompanyNameForForeign")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DegreeId"] = new SelectList(_context.Degrees, "Id", "Name", employee.DegreeId);
            ViewData["SectorId"] = new SelectList(_context.Sectors, "Id", "Name", employee.SectorId);
            return View(employee);
        }

        // GET: Manage/Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Degree)
                .Include(e => e.Sector)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Manage/Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
