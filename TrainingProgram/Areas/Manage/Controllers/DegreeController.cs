using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainingProgram.Areas.Manage.Controllers
{
    [Area("Manage")]
    [AllowAnonymous]
    public class DegreeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
