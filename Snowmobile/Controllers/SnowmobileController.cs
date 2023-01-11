using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Data;
using SnowmobileShop.Models;
using SnowmobileShop.Models.ViewModels;

namespace SnowmobileShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SnowmobileController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHost;

        public SnowmobileController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _dbContext = context;
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            var snowmobiles = _dbContext.Snowmobiles.ToList();
            return View(snowmobiles);
        }

        public IActionResult Create()
        {
            var snowmobileTypes = _dbContext.SnowmobileTypes.ToList();
            var selectListItems = new List<SelectListItem>();

            foreach (var type in snowmobileTypes)
            {
                var selectListItem = new SelectListItem { Value = type.Id.ToString(), Text = type.Name };
                selectListItems.Add(selectListItem);
            }

            var snowmobileVM = new SnowmobileViewModel { Snowmobile = new Models.Snowmobile(), SnowmobileTypes = selectListItems };

            return View(snowmobileVM);
        }

        [HttpPost]
        public IActionResult Create(SnowmobileViewModel snowmobileVM)
        {
            if (!ModelState.IsValid)
                return View(snowmobileVM);

            var wwwrootPath = _webHost.WebRootPath;

            if (wwwrootPath == null)
                throw new ArgumentNullException(nameof(wwwrootPath), "Web root path is null");

            if (snowmobileVM.Snowmobile.Image != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwrootPath, @"img\");
                var extension = Path.GetExtension(snowmobileVM.Snowmobile.Image.FileName);

                if (snowmobileVM.Snowmobile.ImageUrl != null)
                {
                    var path = wwwrootPath + snowmobileVM.Snowmobile.ImageUrl;
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                var fullPath = Path.Combine(uploads, fileName + extension);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    snowmobileVM.Snowmobile.Image.CopyTo(fileStream);
                }

                snowmobileVM.Snowmobile.ImageUrl = @"\img\" + fileName + extension;
            }

            _dbContext.Snowmobiles.Add(snowmobileVM.Snowmobile);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var snowmobileVM = new SnowmobileViewModel();

            snowmobileVM.Snowmobile = _dbContext.Snowmobiles.FirstOrDefault(x => x.Id == id);

            var snowmobileTypes = _dbContext.SnowmobileTypes.ToList();
            var selectListItems = new List<SelectListItem>();

            foreach (var type in snowmobileTypes)
            {
                var selectListItem = new SelectListItem { Value = type.Id.ToString(), Text = type.Name };
                selectListItems.Add(selectListItem);
            }

            snowmobileVM.SnowmobileTypes = selectListItems;

            return View(snowmobileVM);
        }

        [HttpPost]
        public IActionResult Edit(SnowmobileViewModel snowmobileVM)
        {
            if (!ModelState.IsValid)
                return View(snowmobileVM);

            var wwwrootPath = _webHost.WebRootPath;

            if (wwwrootPath == null)
                throw new ArgumentNullException(nameof(wwwrootPath), "Web root path is null");

            if (snowmobileVM.Snowmobile.Image != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwrootPath, @"img\");
                var extension = Path.GetExtension(snowmobileVM.Snowmobile.Image.FileName);

                if (snowmobileVM.Snowmobile.ImageUrl != null)
                {
                    var path = wwwrootPath + snowmobileVM.Snowmobile.ImageUrl;
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                var fullPath = Path.Combine(uploads, fileName + extension);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    snowmobileVM.Snowmobile.Image.CopyTo(fileStream);
                }

                snowmobileVM.Snowmobile.ImageUrl = @"\img\" + fileName + extension;
            }

            var dbSnowmobile = _dbContext.Snowmobiles.FirstOrDefault(x => x.Id == snowmobileVM.Snowmobile.Id);
            
            if (dbSnowmobile != null)
            {
                dbSnowmobile.Name = snowmobileVM.Snowmobile.Name;
                dbSnowmobile.ListPrice = snowmobileVM.Snowmobile.ListPrice;
                dbSnowmobile.Price = snowmobileVM.Snowmobile.Price;
                dbSnowmobile.Description = snowmobileVM.Snowmobile.Description;
                dbSnowmobile.Brand = snowmobileVM.Snowmobile.Brand;
                dbSnowmobile.Model = snowmobileVM.Snowmobile.Model;
                dbSnowmobile.Horsepower = snowmobileVM.Snowmobile.Horsepower;
                dbSnowmobile.EngineCapacity = snowmobileVM.Snowmobile.EngineCapacity;
                dbSnowmobile.YearOfProduction = snowmobileVM.Snowmobile.YearOfProduction;
                dbSnowmobile.SnowmobileTypeId = snowmobileVM.Snowmobile.SnowmobileTypeId;

                if(snowmobileVM.Snowmobile.ImageUrl != null)
                    dbSnowmobile.ImageUrl = snowmobileVM.Snowmobile.ImageUrl;

                _dbContext.Snowmobiles.Update(dbSnowmobile);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var snowmobile = _dbContext.Snowmobiles.FirstOrDefault(x => x.Id == id);
            var selectListItems = new List<SelectListItem>();

            var snowmobileTypes = _dbContext.SnowmobileTypes.ToList();
            foreach (var type in snowmobileTypes)
            {
                var selectListItem = new SelectListItem { Value = type.Id.ToString(), Text = type.Name };
                selectListItems.Add(selectListItem);
            }

            var snowmobileVM = new SnowmobileViewModel { Snowmobile = snowmobile, SnowmobileTypes = selectListItems };

            return View(snowmobileVM);
        }

        [HttpPost]
        public IActionResult Delete(SnowmobileViewModel snowmobileVM)
        {
            if(snowmobileVM.Snowmobile == null)
                return RedirectToAction("Index");

            _dbContext.Snowmobiles.Remove(snowmobileVM.Snowmobile);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
