using System;
using System.ComponentModel.DataAnnotations;

namespace MyFitnessBud.Models
{
    public class SnackCache
    {
        [Key]
        public int SnackCacheId { get; set; }

        [Required]
        public string ProductCode { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public string Allergens { get; set; } // comma-separated
        public bool IngredientsAvailable { get; set; }

        public DateTime LastFetched { get; set; } = DateTime.UtcNow;
    }
}