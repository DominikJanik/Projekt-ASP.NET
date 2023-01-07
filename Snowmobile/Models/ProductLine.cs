#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace SnowmobileShop.Models
{
    public class ProductLine
    {
        public int Id { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("RentalTimeId")]
        public int RentalTimeId { get; set; }
        public RentalTime RentalTime { get; set; }

        public int Quantity { get; set; }
    }
}
