using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SnowmobileShop.Models.ViewModels
{
    public class OrderViewModel
    {
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
