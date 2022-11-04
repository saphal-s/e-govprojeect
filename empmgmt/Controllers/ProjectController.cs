using Microsoft.AspNetCore.Mvc;
using empmgmt.Models;
using Microsoft.AspNetCore.Authorization;

namespace empmgmt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        private readonly EmpDbContext _empDbContext;
        public ProjectController(EmpDbContext empDbContext)
        {
            _empDbContext = empDbContext;
        }

        public IActionResult Index()
        {
            List<ProjectModel> projects = _empDbContext.Projects.ToList();
            return View(projects);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProjectModel model)
        {
            try
            {
                _empDbContext.Projects.Add(model);
                _empDbContext.SaveChanges();
                TempData["ResponseMessage"] = "Project Added Successfully";
                TempData["ResponseValue"] = "1";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ResponseMessage"] = "Project Added Failed!";
                TempData["ResponseValue"] = "0";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            ProjectModel p = _empDbContext.Projects.FirstOrDefault(X => X.Id == id);
            if(p == null)
            {
                p = new ProjectModel();
            }
            return View(p);
        }
        [HttpPost]
        public IActionResult Edit(ProjectModel model)
        {
            try
            {
                _empDbContext.Projects.Update(model);
                _empDbContext.SaveChanges();
                TempData["ResponseMessage"] = "Project Updated Successfully";
                TempData["ResponseValue"] = "1";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["ResponseMessage"] = "Project Update Failed!";
                TempData["ResponseValue"] = "0";
                return View(model);
            }
           
        }
        public IActionResult Delete(string id)
        {
            ProjectModel p = _empDbContext.Projects.FirstOrDefault(x => x.Id == id);
            if( p != null)
            {
                _empDbContext.Projects.Remove(p);
                _empDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
