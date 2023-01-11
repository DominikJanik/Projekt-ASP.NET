using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Data;

namespace SnowmobileShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationDbContext _dbContext;

        public HomeController(IApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var snowmobiles = _dbContext.Snowmobiles.ToList();

            return View(snowmobiles);
        }
    }
}