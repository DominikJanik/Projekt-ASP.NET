using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnowmobileShop.Data;
using SnowmobileShop.Models;
using System.Security.Claims;

namespace SnowmobileShop.Components
{
    public class ShoppingCartSummaryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartSummaryViewComponent(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var shoppingCart = _context.ShoppingCarts
                .Include(x => x.Lines)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.UserId == userId);

            return View(shoppingCart);
        }
    }
}
