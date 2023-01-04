#nullable disable

using System.ComponentModel.DataAnnotations;

namespace SnowmobileShop.Models
{
    public class SnowmobileType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}