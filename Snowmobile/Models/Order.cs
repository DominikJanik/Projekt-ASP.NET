using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SnowmobileShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string? UserId { get; set; }

        public IEnumerable<ProductLine> ProductLines { get; set; }

        public decimal TotalPrice { get; set; }

        [Required]
        public string Forename { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
