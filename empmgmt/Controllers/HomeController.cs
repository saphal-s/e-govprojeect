using empmgmt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace empmgmt.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            ApplicationUser iuser = await _userManager.FindByNameAsync(model.UserName);
            if (iuser != null)
            {
                bool result = await _userManager.CheckPasswordAsync(iuser, model.Password);
                if (result)
                {
                    await _signInManager.SignInAsync(iuser, result);
                    var role = await _userManager.GetRolesAsync(iuser);
                    if (role.Contains("Admin"))
                        return Redirect("/Dashboard/Index");
                    else
                        return Redirect("/Dashboard/Employeedash");
                }
                else
                {
                    return View(model);
                }

            }
            return View(model);
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
}