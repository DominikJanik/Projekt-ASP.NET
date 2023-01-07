using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SnowmobileShop.Models
{
    public class ShoppingCart
    {   
        [Key]
        public int Id { get; set; }
        public IEnumerable<ProductLine> Lines { get; set; }
        public decimal TotalPrice { get; set; }

        public string? UserId { get; set; }
    }
}
