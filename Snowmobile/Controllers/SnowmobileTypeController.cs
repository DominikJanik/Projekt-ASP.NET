using Microsoft.AspNetCore.Mvc;
using SnowmobileShop.Data;
using SnowmobileShop.Models;

namespace SnowmobileShop.Controllers
{
    public class SnowmobileTypeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SnowmobileTypeController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var types = _dbContext.SnowmobileTypes.ToList();

            return View(types);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(SnowmobileType type)
        {
            if (!ModelState.IsValid)
                return View(type);

            _dbContext.SnowmobileTypes.Add(type);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var type = _dbContext.SnowmobileTypes.FirstOrDefault(x => x.Id == id);

            if (type == null)
                return RedirectToAction("Index");

            return View(type);
        }

        [HttpPost]
        public IActionResult Edit(SnowmobileType type)
        {
            var dbType = _dbContext.SnowmobileTypes.FirstOrDefault(x => x.Id == type.Id);

            if (dbType == null)
                return RedirectToAction("Index");

            dbType.Name = type.Name;
            _dbContext.Update(dbType);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var type = _dbContext.SnowmobileTypes.FirstOrDefault(x => x.Id == id);

            if (type == null)
                return RedirectToAction("Index");

            return View(type);
        }

        [HttpPost]
        public IActionResult Delete(SnowmobileType type)
        {
            var dbType = _dbContext.SnowmobileTypes.FirstOrDefault(x => x.Id == type.Id);

            if (dbType == null)
                return RedirectToAction("Index");

            _dbContext.Remove(dbType);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
