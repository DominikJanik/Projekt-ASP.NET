using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SnowmobileShop.Models
{
    public class RentalTime
    {
        public int Id { get; set; }
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }
        public bool IsReserved { get; set; }

        [JsonIgnore]
        [ForeignKey("DayId")]
        public int DayId { get; set; }
        [JsonIgnore]
        public RentalDay RentalDay { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public string? UserId { get; set; }
    }
}