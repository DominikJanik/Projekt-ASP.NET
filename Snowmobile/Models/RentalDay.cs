#nullable disable

using System.Text.Json.Serialization;

namespace SnowmobileShop.Models
{
    public class RentalDay
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public IEnumerable<RentalTime> RentalHours { get; set; }

        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
