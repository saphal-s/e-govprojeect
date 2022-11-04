using empmgmt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace empmgmt.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly EmpDbContext _empDbContext;
        public UserController(EmpDbContext empDbContext, UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _empDbContext = empDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<UserViewModel> users = (from u in _empDbContext.Users
                                         join d in _empDbContext.Departments on
                                              u.DpartmentId equals d.Id
                                         join p in _empDbContext.Positions
          on u.PositionId equals p.Id
                                         //where
                                         select new UserViewModel
                                         {
                                             Id = u.Id,
                                             FirstName = u.FirstName,
                                             LastName = u.LastName,
                                             Email = u.Email,
                                             Address = u.Address,
                                             PhoneNumber = u.PhoneNumber,
                                             Salary = u.Salary,
                                             EmpId = u.EmpId,
                                             DpartmentId = u.DpartmentId,
                                             DepartmentName = d.Name,
                                             PositionId = u.PositionId,
                                             PositionName = p.Name
                                         }).ToList();

            return View(users);
        }
        public async Task<IActionResult> Profile()
        {

            ApplicationUser Cuser = await _userManager.GetUserAsync(HttpContext.User);

           

            List<UserViewModel> users = (from u in _userManager.Users
                                         join d in _empDbContext.Departments on
                                              u.DpartmentId equals d.Id
                                         join p in _empDbContext.Positions
                                                on u.PositionId equals p.Id
                                         where u.Id == Cuser.Id
                                         select new UserViewModel
                                         {
                                             Id = u.Id,
                                             FirstName = u.FirstName,
                                             LastName = u.LastName,
                                             Email = u.Email,
                                             Address = u.Address,
                                             PhoneNumber = u.PhoneNumber,
                                             Salary = u.Salary,
                                             EmpId = u.EmpId,
                                             DpartmentId = u.DpartmentId,
                                             DepartmentName = d.Name,
                                             PositionId = u.PositionId,
                                             PositionName = p.Name
                                         }).ToList();

            return View(users);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            List<DepartmentModel> list = _empDbContext.Departments.ToList();
            List<PositionModel> plist = _empDbContext.Positions.ToList();

            List<SelectListItem> selectList = list
                .Select(X => new SelectListItem { Text = X.Name, Value = X.Id.ToString() })
                .ToList();
            ViewBag.Departments = selectList;

            List<SelectListItem> selectpList = plist
                .Select(X => new SelectListItem { Text = X.Name, Value = X.Id.ToString() })
                .ToList();
            ViewBag.Positions = selectpList;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {

            ApplicationUser User = new ApplicationUser
            {
                EmpId = model.EmpId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                DpartmentId = model.DpartmentId,
                PositionId = model.PositionId,
                Address = model.Address,
                Salary = model.Salary,
                Email = model.Email,
                UserName = model.FirstName,
            };


            IdentityResult result = await _userManager.CreateAsync(User, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(User, model.RoleName);
                return Redirect("/Leave/Userleave");

            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
            public async Task<IActionResult>  Edit(string id)
            {
                List<DepartmentModel> list = _empDbContext.Departments.ToList();
                List<PositionModel> plist = _empDbContext.Positions.ToList();
                List<SelectListItem> selectpList = plist
                    .Select(X => new SelectListItem { Text = X.Name, Value = X.Id.ToString() })
                    .ToList();
                ViewBag.Positions = selectpList;
            List<SelectListItem> selectList = list
                   .Select(X => new SelectListItem { Text = X.Name, Value = X.Id.ToString() })
                   .ToList();
            ViewBag.Departments = selectList;

            ApplicationUser u = await _userManager.FindByIdAsync(id);
            
             return View(u);
            }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult>Edit(ApplicationUser model)
        {
            try
            {
                ApplicationUser u = await _userManager.FindByIdAsync(model.Id);
                u.FirstName = model.FirstName;
                u.LastName = model.LastName;
                u.Email = model.Email;
                u.PhoneNumber = model.PhoneNumber;
                u.PositionId   =  model.PositionId;
                u.DpartmentId = model.DpartmentId;
                u.Address = model.Address;
                u.Salary = model.Salary;
                u.Id = model.Id;
                u.UserName = model.FirstName;

              await _userManager.UpdateAsync(u);
                TempData["ResponseMessage"] = "Employee Updated Successfully";
                TempData["ResponseValue"] = "1";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ResponseMessage"] = "Employee Update Failed!";
                TempData["ResponseValue"] = "0";
                return View(model);
            }
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return Redirect("/Home/Login");
        }
    }
}
