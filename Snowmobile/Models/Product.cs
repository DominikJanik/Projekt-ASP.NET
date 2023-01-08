#nullable disable

using System.ComponentModel.DataAnnotations;

namespace SnowmobileShop.Models
{
    public abstract class Product
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public decimal Price { get; set; }

        public IEnumerable<RentalDay> RentalDays { get; set; }
    }
}
