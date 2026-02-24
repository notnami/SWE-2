// Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyFitnessBudAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}