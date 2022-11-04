using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace empmgmt.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize (Roles="Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Employee")]
        public IActionResult Employeedash()
        {
            return View();
        }

    }
}
