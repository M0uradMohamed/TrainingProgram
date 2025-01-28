using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TraineeController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly ITraineeRepository traineeRepository;
        private readonly IEmployeeRepository employeeRepository;

        public TraineeController(ICourseRepository courseRepository, ITraineeRepository traineeRepository
            , IEmployeeRepository employeeRepository)
        {
            this.courseRepository = courseRepository;
            this.traineeRepository = traineeRepository;
            this.employeeRepository = employeeRepository;
        }
        public IActionResult Index(int id = 1)
        {

            var Iscourse = courseRepository.Get(expression: e => e.Id == id).Any();

            if (!Iscourse)
            {
                return RedirectToAction("Notfound", "Home");

            }
            var course = courseRepository.Get(expression: e => e.Id == id, includeProps: [e => e.PrimaryInstructor]).Select(e => new
            {
                e.Id,
                e.Name,
                e.BeginningDate,
                e.EndingDate,
                PrimaryInstructorName = e.PrimaryInstructor.Name,
                e.ImplementationPlace
            }).FirstOrDefault();

            ViewBag.Course = course;

       /*    var trainees = traineeRepository.Get(expression: e => e.CourseId == id
            , includeProps: [e => e.Employee])
                .Select(e => new
                {
                    EmployeeFoundationId = e.Employee.FoundationId,
                    EmployeeName = e.Employee.Name,
                    e.Employee.Job,
                    e.Employee.WorkPlace,
                    e.Estimate,
                    e.Notes,
                    e.File,
                    e.AbsenceDays,
                    e.AttendanceAndDeparture,
                    e.AdherenceMark,
                    e.InteractionMark,
                    e.ActivitiesMark,
                    e.TotalEvaluation,
                    e.WrittenExam,
                    e.TotalMarks
                }).ToList();*/

            var trainees = traineeRepository.Get(expression: e => e.CourseId == id, includeProps: [e => e.Employee])
       .Select(e => new TraineeVM
       {
           EmployeeFoundationId = e.Employee.FoundationId,
           EmployeeName = e.Employee.Name,
           Job = e.Employee.Job,
           WorkPlace = e.Employee.WorkPlace,
           Estimate = e.Estimate,
           Notes = e.Notes,
           File = e.File,
           AbsenceDays = e.AbsenceDays,
           AttendanceAndDeparture = e.AttendanceAndDeparture,
           AdherenceMark = e.AdherenceMark,
           InteractionMark = e.InteractionMark,
           ActivitiesMark = e.ActivitiesMark,
           TotalEvaluation = e.TotalEvaluation,
           WrittenExam = e.WrittenExam,
           TotalMarks = e.TotalMarks
       }).ToList();
            return View(trainees);
        }
        public IActionResult Create(int id)
        {
            var Iscourse = courseRepository.Get(expression: e => e.Id == id).Any();

            if (!Iscourse)
            {
                return RedirectToAction("Notfound", "Home");

            }
            int courseId = id;

            var employees = employeeRepository.Get().Select(e => new
            {
                e.Id,
                e.FoundationId,
                e.Name,
                e.Job,
                e.WorkPlace
            }).ToList();

            ViewBag.Employees=employees;
            ViewBag.CourseId= courseId;

            return View();
        }
    }
}
