using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SnowmobileShop.Models
{
    public class Trip : Product
    {
        [Required]
        public string Description { get; set; }
        
        [Required]
        public int Hours { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
        
        [NotMapped]
        [ValidateNever]
        public IFormFile Image { get; set; }
    }
}
