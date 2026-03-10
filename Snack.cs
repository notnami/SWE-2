using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFitnessBud.Models;

public class Snack
{
    [Key]
    public int SnackId { get; set; }

    [Required]
    public string ProductCode { get; set; } // OpenFoodFacts barcode

    public int Rating { get; set; }

    public string Name { get; set; }
    public string ImageUrl { get; set; }

    public string Allergens { get; set; } // comma-separated
    public bool IngredientsAvailable { get; set; }

    public DateTime LastFetched { get; set; } = DateTime.UtcNow;
}