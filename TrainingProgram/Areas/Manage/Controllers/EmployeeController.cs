
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

        public EmployeeController(ApplicationDbContext context, IEmployeeRepository employeeRepository,
            IDegreeRepository degreeRepository, ISectorRepository sectorRepository)
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
                return RedirectToAction("Notfound", "Home");
            }

            var employee = employeeRepository.Get(includeProps:
               [ e => e.Degree,
                e => e.Sector]
                , expression: m => m.Id == id)
                .FirstOrDefault();
            if (employee == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            return View(employee);
        }

        // GET: Manage/Employee/Create
        public IActionResult Create()
        {
            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();
            ViewBag.Gender = GenderData.gender;
            ViewBag.Belong = BelongData.belong;
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
                    Name = MethodsCheck.chechName(employeeVM.Name),
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
                if (employee.FoundationId.All(char.IsDigit))
                {
                    if (employeeRepository.Get().Any(e => e.FoundationId == employee.FoundationId))
                    {
                        ModelState.AddModelError("FoundationId", "رقم المؤسسة موجود بالفعل");

                        ViewBag.Degree = degreeRepository.Get().ToList();
                        ViewBag.Sector = sectorRepository.Get().ToList();
                        ViewBag.Gender = GenderData.gender;
                        ViewBag.Belong = BelongData.belong;
                        return View(employeeVM);
                    }

                }
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
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var employee = employeeRepository.Get(includeProps:
              [ e => e.Degree,
                e => e.Sector]
              , expression: m => m.Id == id)
             .FirstOrDefault();
            if (employee == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var employeeVM = new EmployeeVM()
            {
                Id = employee.Id,
                FoundationId = employee.FoundationId,
                Name = employee.Name,
                Gender = employee.Gender,
                Major = employee.Major,
                Job = employee.Job,
                Department = employee.Department,
                SectorId = employee.SectorId,
                DegreeId = employee.DegreeId,
                WorkPlace = employee.WorkPlace,
                PhoneNumber = employee.PhoneNumber,
                Belong = employee.Belong,
                CompanyNameForForeign = employee.CompanyNameForForeign
            };

            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();
            ViewBag.Gender = GenderData.gender;
            ViewBag.Belong = BelongData.belong;
            return View(employeeVM);
        }

        // POST: Manage/Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeVM employeeVM)
        {
            if (id != employeeVM.Id)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {

                if (!EmployeeExists(employeeVM.Id))
                {
                    return RedirectToAction("Notfound", "Home");
                }
                var employee = new Employee()
                {
                    Id = employeeVM.Id,
                    FoundationId = MethodsCheck.chechName(employeeVM.FoundationId),
                    Name = MethodsCheck.chechName(employeeVM.Name),
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
                if (employee.FoundationId.All(char.IsDigit))
                {
                    if (employeeRepository.Get(expression: e => e.FoundationId == employee.FoundationId 
                    && e.Id!=employee.Id).FirstOrDefault() != null )
                    {
                        ModelState.AddModelError("FoundationId", "رقم المؤسسة موجود بالفعل");

                        ViewBag.Degree = degreeRepository.Get().ToList();
                        ViewBag.Sector = sectorRepository.Get().ToList();
                        ViewBag.Gender = GenderData.gender;
                        ViewBag.Belong = BelongData.belong;
                        return View(employeeVM);
                    }

                }
                    employeeRepository.Edit(employee);
                    employeeRepository.Commit();

                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();
            ViewBag.Gender = GenderData.gender;
            ViewBag.Belong = BelongData.belong;
            return View(employeeVM);
        }


        // POST: Manage/Employee/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = employeeRepository.Get(expression: m => m.Id == id).FirstOrDefault();
            if (employee != null)
            {
                employeeRepository.Delete(employee);
            }

            employeeRepository.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}

