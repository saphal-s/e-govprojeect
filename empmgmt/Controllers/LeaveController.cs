using empmgmt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace empmgmt.Controllers
{
    public class LeaveController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly EmpDbContext _empDbContext;
        public LeaveController(EmpDbContext empDbContext, UserManager<ApplicationUser> userManager)
        {
            _empDbContext = empDbContext;
            _userManager = userManager;

        }
       
        public IActionResult Index()
        {

            List<LeaveViewModel> leaves = (from l in _empDbContext.Leaves
                                           join u in _userManager.Users on
                                                l.UserId equals u.Id
                                           //where
                                           select new LeaveViewModel
                                           {
                                               Subject = l.Subject,
                                               Message = l.Message,
                                               Schedule = l.Schedule,
                                               Approve = l.Approve,
                                               Id = l.Id,
                                               UserId = l.UserId,
                                               EmpId = u.EmpId,
                                               FirstName = u.FirstName,
                                               LastName = u.LastName,
                                           }).ToList();

            return View(leaves);
        }
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Userleave()
        {
            ApplicationUser Cuser = await _userManager.GetUserAsync(HttpContext.User);
            List<LeaveViewModel> leaves = (from l in _empDbContext.Leaves
                                           join u in _userManager.Users on
                                                l.UserId equals u.Id
                                           where u.Id == Cuser.Id
                                           select new LeaveViewModel
                                           {
                                               Subject = l.Subject,
                                               Message = l.Message,
                                               Schedule = l.Schedule,
                                               Approve = l.Approve,
                                               Id = l.Id,
                                               UserId = l.UserId,
                                               EmpId = u.EmpId,
                                               FirstName = u.FirstName,
                                               LastName = u.LastName,
                                           }).ToList();

            return View(leaves);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LeaveModel model)
        {

            try
            {
                ApplicationUser User = await _userManager.GetUserAsync(HttpContext.User);
                string Id = User.Id;
                model.UserId = Id;
                _empDbContext.Leaves.Add(model);
                _empDbContext.SaveChanges();
                TempData["ResponseMessage"] = "Leave Sent Successfully";
                TempData["ResponseValue"] = "1";
                // return Id;
                return RedirectToAction("Userleave");

            }
            catch
            {
                TempData["ResponseMessage"] = "Leave Failed!";
                TempData["ResponseValue"] = "0";
                return View();
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            LeaveModel l = _empDbContext.Leaves.FirstOrDefault(X => X.Id == id);

            return View(l);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(LeaveModel model)
        {
            try
            {
                _empDbContext.Leaves.Update(model);
                _empDbContext.SaveChanges();
                TempData["ResponseMessage"] = "Leave Updated Successfully";
                TempData["ResponseValue"] = "1";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ResponseMessage"] = "Leave Update Failed!";
                TempData["ResponseValue"] = "0";
                return View(model);
            }

        }
    }
    }
