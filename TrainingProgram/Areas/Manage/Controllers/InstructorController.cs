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
using Models.StaticData;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class InstructorController : Controller
    {
    
        private readonly IInstructorRepository instructorRepository;
        private readonly IDegreeRepository degreeRepository;
        private readonly ISectorRepository sectorRepository;
        public InstructorController( IInstructorRepository instructorRepository,
            IDegreeRepository degreeRepository, ISectorRepository sectorRepository )
        {
          
            this.instructorRepository = instructorRepository;
            this.degreeRepository = degreeRepository;
            this.sectorRepository = sectorRepository;
        }

        // GET: Manage/Instructor
        public IActionResult Index()
        {
            var instructors = instructorRepository.Get(includeProps: [e => e.Degree!, e => e.Sector! ]).ToList();
            return View(instructors);
        }

        // GET: Manage/Instructor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }
            var instructor = instructorRepository.Get(includeProps:
               [ e => e.Degree!,
                e => e.Sector!]
                , expression: m => m.Id == id)
                .FirstOrDefault();
            if (instructor == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            return View(instructor);
        }

        // GET: Manage/Instructor/Create
        public IActionResult Create()
        {
            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();
          
            return View();
        }

        // POST: Manage/Instructor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( InstructorVM instructorVM)
        {
            if (ModelState.IsValid)
            {
                var instructor = new Instructor()
                {
                    FoundationId = MethodsCheck.chechName(instructorVM.FoundationId),
                    Name=MethodsCheck.chechName(instructorVM.Name),
                    BirthDate = instructorVM.BirthDate,
                    HiringDate = instructorVM.HiringDate,
                    DegreeId = instructorVM.DegreeId,
                    Department = MethodsCheck.chechName(instructorVM.Department),
                    SectorId = instructorVM.SectorId,
                    WorkPlace = MethodsCheck.chechName(instructorVM.WorkPlace),
                    Major = MethodsCheck.chechName(instructorVM.Major),
                    GraduationeDate = instructorVM.GraduationeDate,
                    AcademicDegree = instructorVM.AcademicDegree,
                    AcademicDegreeeDate = instructorVM.AcademicDegreeeDate,
                    PhoneNumber = instructorVM.PhoneNumber,
                    OtherPhoneNumber = instructorVM.OtherPhoneNumber,
                    Email = instructorVM.Email,


                };
                if (instructor.FoundationId.All(char.IsDigit))
                {
                    if (instructorRepository.Get().Any(e => e.FoundationId == instructor.FoundationId))
                    {
                        ModelState.AddModelError("FoundationId", "رقم المؤسسة موجود بالفعل");

                        ViewBag.Degree = degreeRepository.Get().ToList();
                        ViewBag.Sector = sectorRepository.Get().ToList();

                        return View(instructorVM);
                    }

                }


                instructorRepository.Create(instructor);
                instructorRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Degree = degreeRepository.Get().ToList();
            ViewBag.Sector = sectorRepository.Get().ToList();
            return View(instructorVM);
        }

        // GET: Manage/Instructor/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var instructor = instructorRepository.Get(includeProps:
              [ e => e.Degree,
                e => e.Sector]
               , expression: m => m.Id == id)
               .FirstOrDefault();
            if (instructor == null)
            {
                return RedirectToAction("Notfound", "Home");
            }

            var instructorVM = new InstructorVM()
            {
                Id = instructor.Id,
                FoundationId = instructor.FoundationId,
                Name = instructor.Name,
                BirthDate = instructor.BirthDate,
                DegreeId= instructor.DegreeId,
                SectorId= instructor.SectorId,
                AcademicDegree = instructor.AcademicDegree,
                AcademicDegreeeDate = instructor.AcademicDegreeeDate,
                Department=instructor.Department,
                GraduationeDate= instructor.GraduationeDate,
                HiringDate= instructor.HiringDate,
                Email = instructor.Email,
                Major= instructor.Major,
                OtherPhoneNumber= instructor.OtherPhoneNumber,
                PhoneNumber= instructor.PhoneNumber,
                WorkPlace= instructor.WorkPlace,

            };


            ViewBag.Degree = degreeRepository.Get().Select(e=> new { e.Id, e.Name }).ToList();
            ViewBag.Sector = sectorRepository.Get().Select(e => new { e.Id, e.Name }).ToList();
            return View(instructorVM);
        }

        // POST: Manage/Instructor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, InstructorVM instructorVM)
        {
            if (id != instructorVM.Id)
            {
                return RedirectToAction("Notfound", "Home");
            }

            if (ModelState.IsValid)
            {
               
               
                    if (!InstructorExists(instructorVM.Id))
                    {
                        return RedirectToAction("Notfound", "Home");
                    }
                var instructor = new Instructor()
                {
                    Id = instructorVM.Id,
                    FoundationId = MethodsCheck.chechName(instructorVM.FoundationId),
                    Name = MethodsCheck.chechName(instructorVM.Name),
                    BirthDate = instructorVM.BirthDate,
                    HiringDate = instructorVM.HiringDate,
                    DegreeId = instructorVM.DegreeId,
                    Department = MethodsCheck.chechName(instructorVM.Department),
                    SectorId = instructorVM.SectorId,
                    WorkPlace = MethodsCheck.chechName(instructorVM.WorkPlace),
                    Major = MethodsCheck.chechName(instructorVM.Major),
                    GraduationeDate = instructorVM.GraduationeDate,
                    AcademicDegree = instructorVM.AcademicDegree,
                    AcademicDegreeeDate = instructorVM.AcademicDegreeeDate,
                    PhoneNumber = instructorVM.PhoneNumber,
                    OtherPhoneNumber = instructorVM.OtherPhoneNumber,
                    Email = instructorVM.Email,
                };
                if (instructor.FoundationId.All(char.IsDigit))
                {
                    if (instructorRepository.Get(expression: e => e.FoundationId == instructor.FoundationId
                    && e.Id != instructor.Id, tracked :false).FirstOrDefault() != null)
                    {
                        ModelState.AddModelError("FoundationId", "رقم المؤسسة موجود بالفعل");

                        ViewBag.Degree = degreeRepository.Get().Select(e => new { e.Id, e.Name }).ToList();
                        ViewBag.Sector = sectorRepository.Get().Select(e => new { e.Id, e.Name }).ToList();
                        return View(instructorVM);
                    }

                }
                instructorRepository.Edit(instructor);
                instructorRepository.Commit();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Degree = degreeRepository.Get().Select(e => new { e.Id, e.Name }).ToList();
            ViewBag.Sector = sectorRepository.Get().Select(e => new { e.Id, e.Name }).ToList();
            return View(instructorVM);
        }


        // POST: Manage/Instructor/Delete/5
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var instructor = instructorRepository.Get(expression: m => m.Id == id).FirstOrDefault();
            if (instructor != null)
            {
                instructorRepository.Delete(instructor);
            }

            instructorRepository.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return instructorRepository.Get(tracked:false).Any(e => e.Id == id);
        }
    }
}
