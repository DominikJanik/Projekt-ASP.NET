using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Data;
using SnowmobileShop.Models;
using SnowmobileShop.Models.ViewModels;
using SnowmobileShop.Services;
using System.Security.Claims;

namespace SnowmobileShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IApplicationIdentityDbContext _identityDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IShoppingCartService _shoppingCartService;


        public OrderController(IApplicationDbContext dbContext, UserManager<IdentityUser> userManager, IShoppingCartService cartService, IApplicationIdentityDbContext identityDbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _shoppingCartService = cartService;
            _identityDbContext = identityDbContext;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _identityDbContext.Users.First(x => x.Id == userId);

            var shoppingCart = _dbContext.ShoppingCarts
                .Include(x => x.Lines)
                .ThenInclude(x => x.Product)
                .Include(x => x.Lines)
                .ThenInclude(x => x.RentalTime)
                .ThenInclude(x => x.RentalDay)
                .FirstOrDefault(x => x.UserId == userId);

            if(shoppingCart == null)
                return RedirectToAction("Index", "Home");

            var orderViewModel = new OrderViewModel()
            {
                ShoppingCartId = shoppingCart.Id,
                ShoppingCart = shoppingCart,
                UserId = userId,
                User = user
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult Index(OrderViewModel orderViewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shoppingCart = _dbContext.ShoppingCarts
                .Include(x => x.Lines)
                .ThenInclude(x => x.Product)
                .Include(x => x.Lines)
                .ThenInclude(x => x.RentalTime)
                .ThenInclude(x => x.RentalDay)
                .FirstOrDefault(x => x.UserId == userId);

            orderViewModel.ShoppingCart = shoppingCart;

            if(!ModelState.IsValid)
                return View(orderViewModel);

            foreach (var line in shoppingCart.Lines)
            {
                line.RentalTime.IsReserved = true;
                line.RentalTime.UserId = userId;
                _dbContext.SaveChanges();
            }

            List<Product> products = new List<Product>();

            foreach (var line in shoppingCart.Lines)
            {
                products.Add(line.Product);
            }

            var order = new Order()
            {
               UserId = userId,
               TotalPrice = shoppingCart.TotalPrice,
               Forename = orderViewModel.User.Forename,
               Surname = orderViewModel.User.Surname,
               PhoneNumber = orderViewModel.User.PhoneNumber,
               Email = orderViewModel.User.Email,
               ProductLines = shoppingCart.Lines
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            _shoppingCartService.Clear(shoppingCart);
            _dbContext.SaveChanges();

            return View("Success", order);
        }

        public IActionResult MyTickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = _dbContext.Orders
                .Include(x => x.ProductLines)
                .ThenInclude(x => x.Product)
                .Include(x => x.ProductLines)
                .ThenInclude(x => x.RentalTime)
                .ThenInclude(x => x.RentalDay)
                .Where(x => x.UserId == userId);

            return View(orders);
        }
    }
}
