// Models/Favorite.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFitnessBudAPI.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OpenFoodFactsId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}