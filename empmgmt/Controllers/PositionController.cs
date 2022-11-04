using empmgmt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace empmgmt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        private readonly EmpDbContext _empDbContext;
        public PositionController(EmpDbContext empDbContext)
        {
            _empDbContext = empDbContext;
        }

        public IActionResult Index()
        {
            List<PositionModel> positions = _empDbContext.Positions.ToList();
            return View(positions);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PositionModel model)
        {
            try
            {
                    _empDbContext.Positions.Add(model);

                    _empDbContext.SaveChanges();
                    TempData["ResponseMessage"] = "Position Added Successfully";
                    TempData["ResponseValue"] = "1";
                    return RedirectToAction("Index");
            }
            catch
            {
                TempData["ResponseMessage"] = "Position Added Failed!";
                TempData["ResponseValue"] = "0";
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            PositionModel ps = _empDbContext.Positions.FirstOrDefault(X => X.Id == id);
            if (ps == null)
            {
                ps = new PositionModel();
            }
            return View(ps);
        }
        [HttpPost]
        public IActionResult Edit(PositionModel model)
        {
            try {
            _empDbContext.Positions.Update(model);
            _empDbContext.SaveChanges();

                TempData["ResponseMessage"] = "Position Updated Successfully";
                TempData["ResponseValue"] = "1";
                return RedirectToAction("Index");

            }
            catch
            {
                TempData["ResponseMessage"] = "Position Update Failed!";
                TempData["ResponseValue"] = "0";
                return View(model);
            }
        }
        public IActionResult Delete(string id)
        {
            PositionModel ps = _empDbContext.Positions.FirstOrDefault(x => x.Id == id);
            if (ps != null)
            {
                _empDbContext.Positions.Remove(ps);
                _empDbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
