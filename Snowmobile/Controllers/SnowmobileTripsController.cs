using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Data;

namespace SnowmobileShop.Controllers
{
    public class SnowmobileTripsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SnowmobileTripsController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var trips = _dbContext.Trips.ToList();

            return View(trips);
        }

        public IActionResult Details(int id)
        {
            var trip = _dbContext.Trips.FirstOrDefault(x => x.Id == id);
            return View(trip);
        }
    }
}
