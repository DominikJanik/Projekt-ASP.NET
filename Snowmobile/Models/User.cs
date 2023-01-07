using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace SnowmobileShop.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Forename { get; set; }

        [Required]
        public string Surname { get; set; }
    }
}
