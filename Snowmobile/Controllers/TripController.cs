using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SnowmobileShop.Data;
using SnowmobileShop.Models;
using SnowmobileShop.Models.ViewModels;
using System.Data;

namespace SnowmobileShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TripController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHost;

        public TripController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _dbContext = context;
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            var trips = _dbContext.Trips.ToList();

            return View(trips);
        }

        public IActionResult Create()
        {
            return View(new Trip());
        }

        [HttpPost]
        public IActionResult Create(Trip trip)
        {
            if (!ModelState.IsValid)
                return View(trip);

            var wwwrootPath = _webHost.WebRootPath;

            if (wwwrootPath == null)
                throw new ArgumentNullException(nameof(wwwrootPath), "Web root path is null");

            if (trip.Image != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwrootPath, @"img\");
                var extension = Path.GetExtension(trip.Image.FileName);

                if (trip.ImageUrl != null)
                {
                    var path = wwwrootPath + trip.ImageUrl;
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                var fullPath = Path.Combine(uploads, fileName + extension);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    trip.Image.CopyTo(fileStream);
                }

                trip.ImageUrl = @"\img\" + fileName + extension;
            }

            _dbContext.Trips.Add(trip);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var trip = _dbContext.Trips.FirstOrDefault(x => x.Id == id);

            return View(trip);
        }

        [HttpPost]
        public IActionResult Edit(Trip trip)
        {
            if (!ModelState.IsValid)
                return View(trip);

            var wwwrootPath = _webHost.WebRootPath;

            if (wwwrootPath == null)
                throw new ArgumentNullException(nameof(wwwrootPath), "Web root path is null");

            if (trip.Image != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwrootPath, @"img\");
                var extension = Path.GetExtension(trip.Image.FileName);

                if (trip.ImageUrl != null)
                {
                    var path = wwwrootPath + trip.ImageUrl;
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                var fullPath = Path.Combine(uploads, fileName + extension);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    trip.Image.CopyTo(fileStream);
                }

                trip.ImageUrl = @"\img\" + fileName + extension;
            }

            var dbTrip = _dbContext.Trips.FirstOrDefault(x => x.Id == trip.Id);
            
            if (dbTrip != null)
            {
                dbTrip.Name = trip.Name;
                dbTrip.Price = trip.Price;
                dbTrip.Description = trip.Description;
                dbTrip.Hours = trip.Hours;

                if(trip.ImageUrl != null)
                    dbTrip.ImageUrl = trip.ImageUrl;

                _dbContext.Trips.Update(dbTrip);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var trip = _dbContext.Trips.FirstOrDefault(x => x.Id == id);

            return View(trip);
        }

        [HttpPost]
        public IActionResult Delete(Trip trip)
        {
            if(trip == null)
                return RedirectToAction("Index");

            _dbContext.Trips.Remove(trip);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
