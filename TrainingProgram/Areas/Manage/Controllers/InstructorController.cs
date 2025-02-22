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
        private readonly ICourseInstructorRepository courseInstructorRepository;

        public InstructorController( IInstructorRepository instructorRepository,
            IDegreeRepository degreeRepository, ISectorRepository sectorRepository 
            ,ICourseInstructorRepository courseInstructorRepository)
        {
          
            this.instructorRepository = instructorRepository;
            this.degreeRepository = degreeRepository;
            this.sectorRepository = sectorRepository;
            this.courseInstructorRepository = courseInstructorRepository;
        }

        // GET: Manage/Instructor
        public IActionResult Index(string? Search , int page=1 )
        {
            var qinstructors = instructorRepository.Get(includeProps: [e => e.Degree!, e => e.Sector! ]);
            if(Search != null)
            {
                qinstructors =qinstructors.Where( e => e.Name!.Contains( Search.TrimStart().TrimEnd() ) || e.FoundationId!.Contains( Search.TrimStart().TrimEnd() )
                    || e.PhoneNumber!.Contains(Search.TrimStart().TrimEnd()) );
            }
            double totalPages = Math.Ceiling((double)qinstructors.Count() / 20);
            qinstructors = qinstructors.Skip((page - 1) * 20).Take(20);

         IQueryable<InstructorVM>instructors = qinstructors.Select(e => new InstructorVM
            {
                Id = e.Id,
                Name = e.Name,
                FoundationId = e.FoundationId,
                AcademicDegree = e.AcademicDegree,
                AcademicDegreeeDate = e.AcademicDegreeeDate,
                Email = e.Email,
                BirthDate = e.BirthDate,
                HiringDate = e.HiringDate,
                GraduationeDate = e.GraduationeDate,
                Major=e.Major,
                PhoneNumber=e.PhoneNumber,
                OtherPhoneNumber=e.OtherPhoneNumber,
                Status=e.Status,
                WorkPlace=e.WorkPlace,
                Department=e.Department,
                Degree = e.Degree!.Name,
                Sector=e.Sector!.Name
                
            });

            ViewBag.Pages = new { page, totalPages };
            ViewBag.Search = Search;
            return View(instructors.ToList() );
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
                , expression: m => m.Id == id).Select(e => new InstructorVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    FoundationId = e.FoundationId,
                    AcademicDegree = e.AcademicDegree,
                    AcademicDegreeeDate = e.AcademicDegreeeDate,
                    Email = e.Email,
                    BirthDate = e.BirthDate,
                    HiringDate = e.HiringDate,
                    GraduationeDate = e.GraduationeDate,
                    Major = e.Major,
                    PhoneNumber = e.PhoneNumber,
                    OtherPhoneNumber = e.OtherPhoneNumber,
                    Status = e.Status,
                    WorkPlace = e.WorkPlace,
                    Department = e.Department,
                    Degree = e.Degree!.Name,
                    Sector = e.Sector!.Name

                })
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
                    Status = instructorVM.Status

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
              [ e => e.Degree!,
                e => e.Sector!]
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
                Status= instructor.Status,

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
                    Status=instructorVM.Status
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
        public IActionResult Courses(int id,int page=1)
        {
            var instructor = instructorRepository.Get(expression: e => e.Id == id).Select(e => new
            {
                e.Id,
                e.Name,
                e.FoundationId,
                e.PhoneNumber,
                e.Status
            }).FirstOrDefault();
            ViewBag.Instructor = instructor;

            IQueryable<InstructorCoursesVM> courses = courseInstructorRepository.Get(expression: e => e.InstructorId == id, includeProps: [e => e.Course])
                .Select(e => new InstructorCoursesVM
                {
                 CourseId =   e.CourseId,
                 Name =  e.Course.Name,
                 BeginningDate =    e.Course.BeginningDate,
                 EndingDate =   e.Course.EndingDate,
                 Rating =   e.Rating 
                }).OrderBy(e => e.BeginningDate).ThenBy(e => e.EndingDate);

            double totalPages = Math.Ceiling((double)courses.Count() / 5);
            courses = courses.Skip((page - 1) * 5).Take(5);

            ViewBag.Pages = new { page, totalPages };

            return View(courses.ToList());

        }
        private bool InstructorExists(int id)
        {
            return instructorRepository.Get(tracked:false).Any(e => e.Id == id);
        }
    }
}
