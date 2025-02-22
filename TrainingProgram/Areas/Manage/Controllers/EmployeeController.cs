
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
using Models.EnumClasses;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ITraineeRepository traineeRepository;
        private readonly IDegreeRepository degreeRepository;
        private readonly ISectorRepository sectorRepository;

        public EmployeeController(IEmployeeRepository employeeRepository, ITraineeRepository traineeRepository,
            IDegreeRepository degreeRepository, ISectorRepository sectorRepository)
        {

            this.employeeRepository = employeeRepository;
            this.traineeRepository = traineeRepository;
            this.degreeRepository = degreeRepository;
            this.sectorRepository = sectorRepository;
        }

        // GET: Manage/Employee
        public IActionResult Index(string? FoundationId, string? Name, string? Job, string? Department
           , int? SectorId, int? DegreeId, string? WorkPlace , int page=1)
        {
            var qemployees = employeeRepository.Get(includeProps: [e => e.Degree!, e => e.Sector!]);
                

            if (FoundationId != null)
            {
                qemployees = qemployees.Where(e => e.FoundationId!.Contains(FoundationId.TrimStart().TrimEnd() ) );
            }
            if (Name != null)
            {
                qemployees = qemployees.Where(e => e.Name!.Contains(Name.TrimStart().TrimEnd()));
            }
            if (Job != null)
            {
                qemployees = qemployees.Where(e => e.Job!.Contains(Job.TrimStart().TrimEnd()));
            }
            if (Department != null)
            {
                qemployees = qemployees.Where(e => e.Department!.Contains(Department.TrimStart().TrimEnd()));
            }
            if (SectorId != null)
            {
                qemployees = qemployees.Where(e => e.SectorId == SectorId);
            }
            if (DegreeId != null)
            {
                qemployees = qemployees.Where(e => e.DegreeId == DegreeId);
            }
            if (WorkPlace != null)
            {
                qemployees = qemployees.Where(e => e.WorkPlace!.Contains(WorkPlace.TrimStart().TrimEnd()));
            }
            
            var employees = qemployees.Select(e => new EmployeeVM
            {
                Id = e.Id,
                Name = e.Name,
                Gender = e.Gender,
                Belong = e.Belong,
                FoundationId = e.FoundationId,
                Department = e.Department,
                Job = e.Job,
                PhoneNumber = e.PhoneNumber,
                WorkPlace = e.WorkPlace,
                Major = e.Major,
                CompanyNameForForeign = e.CompanyNameForForeign,
                Degree = e.Degree!.Name,
                Sector = e.Sector!.Name
            });

            var Search = new
            {
                FoundationId,
                Name,
                Job,
                Department,
                SectorId,
                DegreeId,
                WorkPlace
            };
           double totalPages = Math.Ceiling((double)employees.Count() / 50);
        
            employees = employees.Skip((page - 1) * 50).Take(50);

            ViewBag.Pages = new { page, totalPages };

            ViewBag.Search = Search;
            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();

            return View(employees.ToList());
        }

        // GET: Manage/Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var employee = employeeRepository.Get(includeProps:
               [ e => e.Degree!,
                e => e.Sector!]
                , expression: m => m.Id == id).Select(e => new EmployeeVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    Gender = e.Gender,
                    Belong = e.Belong,
                    FoundationId = e.FoundationId,
                    Department = e.Department,
                    Job = e.Job,
                    PhoneNumber = e.PhoneNumber,
                    WorkPlace = e.WorkPlace,
                    Major = e.Major,
                    CompanyNameForForeign = e.CompanyNameForForeign,
                    Degree = e.Degree!.Name,
                    Sector = e.Sector!.Name
                })
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
            ViewBag.Gender = StaticData.gender;
            ViewBag.Belong = StaticData.belong;
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
                        ViewBag.Gender = StaticData.gender;
                        ViewBag.Belong = StaticData.belong;
                        return View(employeeVM);
                    }

                }
                employeeRepository.Create(employee);
                employeeRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();
            ViewBag.Gender = StaticData.gender;
            ViewBag.Belong = StaticData.belong;
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
              [ e => e.Degree!,
                e => e.Sector!]
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
            ViewBag.Gender = StaticData.gender;
            ViewBag.Belong = StaticData.belong;
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
                    && e.Id != employee.Id, tracked: false).FirstOrDefault() != null)
                    {
                        ModelState.AddModelError("FoundationId", "رقم المؤسسة موجود بالفعل");

                        ViewBag.Degree = degreeRepository.Get().ToList();
                        ViewBag.Sector = sectorRepository.Get().ToList();
                        ViewBag.Gender = StaticData.gender;
                        ViewBag.Belong = StaticData.belong;
                        return View(employeeVM);
                    }

                }
                employeeRepository.Edit(employee);
                employeeRepository.Commit();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();
            ViewBag.Gender = StaticData.gender;
            ViewBag.Belong = StaticData.belong;
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

                employeeRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Notfound", "Home");
        }
        public IActionResult Courses(int id , int page =1 )
        {
            var employee = employeeRepository.Get(expression: e => e.Id == id).Select(e => new
            {
                e.Id,
                e.Name,
                e.FoundationId,
                e.Job,
                e.WorkPlace,
            }).FirstOrDefault();
            ViewBag.Employee = employee;

            IQueryable<TraineeCourseVM> courses = traineeRepository.Get(expression: e => e.EmployeeId == id, includeProps: [e => e.Course])
                .Select(e => new TraineeCourseVM
                {
                    CourseId = e.CourseId,
                    CourseName = e.Course.Name!,
                    BeginningDate = e.Course.BeginningDate,
                    EndingDate = e.Course.EndingDate,
                    Estimate = e.Estimate,
                    Notes = e.Notes,
                    File = e.File,
                    AbsenceDays = e.AbsenceDays,
                    AttendanceAndDeparture = e.AttendanceAndDeparture,
                    ActivitiesMark = e.ActivitiesMark,
                    AdherenceMark = e.AdherenceMark,
                    InteractionMark = e.InteractionMark,
                    TotalEvaluation = e.TotalEvaluation,
                    WrittenExam = e.WrittenExam,
                    TotalMarks = e.TotalMarks

                }).OrderBy(e => e.BeginningDate).ThenBy(e => e.EndingDate);

            double totalPages = Math.Ceiling((double)courses.Count() / 5);
            courses = courses.Skip((page - 1) * 5).Take(5);

            ViewBag.Pages = new { page, totalPages };


            return View(courses.ToList());

        }


        private bool EmployeeExists(int id)
        {
            return employeeRepository.Get(tracked: false).Any(e => e.Id == id);
        }
    }
}

