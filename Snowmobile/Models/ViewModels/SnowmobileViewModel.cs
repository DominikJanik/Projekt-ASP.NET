#nullable disable

using Microsoft.AspNetCore.Mvc.Rendering;

namespace SnowmobileShop.Models.ViewModels
{
    public class SnowmobileViewModel
    {
        public Snowmobile Snowmobile { get; set; }
        public IEnumerable<SelectListItem> SnowmobileTypes { get; set; }
    }
}
