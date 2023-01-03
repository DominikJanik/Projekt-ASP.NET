using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Data;

namespace SnowmobileShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var snowmobiles = _dbContext.Snowmobiles.ToList();

            return View(snowmobiles);
        }

        public IActionResult Details(int id)
        {
            var snowmobile = _dbContext.Snowmobiles.Include("SnowmobileType").FirstOrDefault(x => x.Id == id);
            return View(snowmobile);
        }
    }
}