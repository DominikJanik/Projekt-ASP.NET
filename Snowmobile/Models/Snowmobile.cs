#nullable disable

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnowmobileShop.Models
{
    public class Snowmobile
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Horsepower { get; set; }

        [Required]
        [DisplayName("Engine Capacity")]
        public string EngineCapacity { get; set; }

        [Required]
        [Range(1900, 2030)]
        [DisplayName("Year Of Production")]
        public int YearOfProduction { get; set; }

        [Required]
        [DisplayName("Snowmobile Type")]
        public int SnowmobileTypeId { get; set; }
        public SnowmobileType SnowmobileType { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        [NotMapped]
        [ValidateNever]
        public IFormFile Image { get; set; }
    }
}
