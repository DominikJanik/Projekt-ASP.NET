using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Data;
using SnowmobileShop.Models;
using SnowmobileShop.Services;
using System.Security.Claims;

namespace SnowmobileShop.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationIdentityDbContext _identityDbContext;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(ApplicationDbContext context, IShoppingCartService shoppingCartService)
        {
            _dbContext = context;
            _shoppingCartService = shoppingCartService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shoppingCart = _dbContext.ShoppingCarts
                .Include(x => x.Lines)
                .ThenInclude(x => x.Product)
                .Include(x => x.Lines)
                .ThenInclude(x => x.RentalTime)
                .ThenInclude(x => x.RentalDay)
                .FirstOrDefault(x => x.UserId == userId);

            return View(shoppingCart);
        }

        public class CartRequestBody
        {
            public int ProductId { get; set; }
            public int HourId { get; set; }
        }

        public IActionResult AddToCart([FromBody] CartRequestBody body)
        {
            var hour = _dbContext.RentalHours.FirstOrDefault(x => x.Id == body.HourId);

            if(hour == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
                return Unauthorized();

            var cartFromDb = _dbContext.ShoppingCarts
                .Include(x => x.Lines)
                .ThenInclude(x => x.Product)
                .Include(x => x.Lines)
                .ThenInclude(x => x.RentalTime)
                .FirstOrDefault(x => x.UserId == userId);

            if (cartFromDb == null)
            {
                cartFromDb = new ShoppingCart();
                cartFromDb.UserId = userId;
                cartFromDb.Lines = new List<ProductLine>();
                _dbContext.ShoppingCarts.Add(cartFromDb);
            }

            var product = _dbContext.Products.FirstOrDefault(p => p.Id == body.ProductId);

            if (product == null)
                return NotFound();

            _shoppingCartService.AddItem(cartFromDb, product, 1, hour);
            cartFromDb.TotalPrice = _shoppingCartService.ComputeTotalValue(cartFromDb);
            _dbContext.SaveChanges();

            return Ok();
        }

        public IActionResult RemoveFromCart(int productId, int timeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = _dbContext.ShoppingCarts
                .Include(x => x.Lines)
                .ThenInclude(x => x.Product)
                .Include(x => x.Lines)
                .ThenInclude(x => x.RentalTime)
                .FirstOrDefault(x => x.UserId == userId);

            var product = _dbContext.Products.FirstOrDefault(p => p.Id == productId);
            var rentalHour = _dbContext.RentalHours.FirstOrDefault(x => x.Id == timeId);

            if (product != null && rentalHour != null)
            {
                _shoppingCartService.RemoveLine(cart, product, rentalHour);
                cart.TotalPrice = _shoppingCartService.ComputeTotalValue(cart);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
