using empmgmt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace empmgmt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
    {
        private readonly EmpDbContext _empDbContext;
        public DepartmentController(EmpDbContext empDbContext)
        {
            _empDbContext = empDbContext;
        }

        public IActionResult Index()
        {
            List<DepartmentViewModel> departments = (from d in _empDbContext.Departments
                                               join p in _empDbContext.Projects on
                                                    d.ProjectId equals p.Id
                                               //where
                                               select new DepartmentViewModel
                                               {
                                                   Name = d.Name,
                                                   Id = d.Id,
                                                   ProjectId = d.ProjectId,
                                                   ProjectName = p.Name
                                               }).ToList();

            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<ProjectModel> list = _empDbContext.Projects.ToList();
            List<SelectListItem> selectList = list
                .Select( X => new SelectListItem { Text = X.Name,Value = X.Id.ToString()})
                .ToList();
            ViewBag.Projects = selectList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentModel model)
        {

            try
            {
                _empDbContext.Departments.Add(model);
                _empDbContext.SaveChanges();
                _empDbContext.SaveChanges();
                TempData["ResponseMessage"] = "Department Added Successfully";
                TempData["ResponseValue"] = "1";
                return RedirectToAction("Index");

            }
            catch
            {
                TempData["ResponseMessage"] = "Department Added Failed!";
                TempData["ResponseValue"] = "0";
                return View();
            }
           
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            List<ProjectModel> list = _empDbContext.Projects.ToList();
            List<SelectListItem> selectList = list
                .Select(X => new SelectListItem { Text = X.Name, Value = X.Id.ToString() })
                .ToList();
            ViewBag.Projects = selectList;
            DepartmentModel d = _empDbContext.Departments.FirstOrDefault(X => X.Id == id);
            if (d == null)
            {
                d = new DepartmentModel();
            }
            return View(d);
        }
        [HttpPost]
        public IActionResult Edit(DepartmentModel model)
        {
            try
            {
                _empDbContext.Departments.Update(model);
                _empDbContext.SaveChanges();
                TempData["ResponseMessage"] = "Department Updated Successfully";
                TempData["ResponseValue"] = "1";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ResponseMessage"] = "Department Update Failed!";
                TempData["ResponseValue"] = "0";
                return View(model);
            }
        }
        public IActionResult Delete(string id)
        {
            DepartmentModel d = _empDbContext.Departments.FirstOrDefault(x => x.Id == id);
            if (d != null)
            {
                _empDbContext.Departments.Remove(d);
                _empDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
