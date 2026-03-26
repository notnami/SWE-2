using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFitnessBud.Data;
using MyFitnessBud.Models;
using MyFitnessBud.Services;
using System.Text.Json;

namespace MyFitnessBud.Controllers.Api;

[ApiController]
[Route("api/off")]
public class OpenFoodFactsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly OpenFoodFactsClient _off;

    public OpenFoodFactsController(ApplicationDbContext db, OpenFoodFactsClient off)
    {
        _db = db;
        _off = off;
    }

    // GET /api/off/product/3017624010701
    [HttpGet("product/{barcode}")]
    public async Task<IActionResult> GetOrFetch(string barcode, CancellationToken ct)
    {
        // Use cache for 7 days
        var cached = await _db.SnackCaches.FirstOrDefaultAsync(s => s.ProductCode == barcode, ct);
        if (cached != null && cached.LastFetched > DateTime.UtcNow.AddDays(-7))
            return Ok(cached);

        var doc = await _off.GetProductAsync(barcode, ct);
        if (doc is null) return NotFound(new { message = "Product not found or OFF request failed." });

        var root = doc.RootElement;
        if (!root.TryGetProperty("product", out var product))
            return NotFound(new { message = "No product payload in response." });

        string? GetString(string name) =>
            product.TryGetProperty(name, out var el) && el.ValueKind == JsonValueKind.String ? el.GetString() : null;

        string allergensCsv = "";
        if (product.TryGetProperty("allergens_tags", out var allergensEl) && allergensEl.ValueKind == JsonValueKind.Array)
        {
            var tags = allergensEl.EnumerateArray()
                .Where(x => x.ValueKind == JsonValueKind.String)
                .Select(x => x.GetString())
                .Where(x => !string.IsNullOrWhiteSpace(x));
            allergensCsv = string.Join(",", tags!);
        }

        var ingredientsText = GetString("ingredients_text");

        var row = cached ?? new SnackCache { ProductCode = barcode };
        row.Name = GetString("product_name") ?? "(unknown)";
        row.ImageUrl = GetString("image_url") ?? "";
        row.Allergens = allergensCsv;
        row.IngredientsAvailable = !string.IsNullOrWhiteSpace(ingredientsText);
        row.LastFetched = DateTime.UtcNow;

        if (cached == null) _db.SnackCaches.Add(row);

        await _db.SaveChangesAsync(ct);
        return Ok(row);
    }
    [HttpGet("search")]
    public async Task<IActionResult> Search(string q, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(q))
            return BadRequest(new { message = "Search query is required." });

        var doc = await _off.SearchProductsAsync(q, 12, ct);
        if (doc is null)
            return StatusCode(502, new { message = "OpenFoodFacts search failed." });

        var root = doc.RootElement;

        if (!root.TryGetProperty("products", out var products) || products.ValueKind != JsonValueKind.Array)
            return Ok(Array.Empty<object>());

        var results = new List<object>();

        foreach (var product in products.EnumerateArray())
        {
            string? GetString(string name) =>
                product.TryGetProperty(name, out var el) && el.ValueKind == JsonValueKind.String
                    ? el.GetString()
                    : null;

            var code = GetString("code");
            var name = GetString("product_name");

            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(name))
                continue;

            string allergensCsv = "";
            if (product.TryGetProperty("allergens_tags", out var allergensEl) &&
                allergensEl.ValueKind == JsonValueKind.Array)
            {
                var tags = allergensEl.EnumerateArray()
                    .Where(x => x.ValueKind == JsonValueKind.String)
                    .Select(x => x.GetString())
                    .Where(x => !string.IsNullOrWhiteSpace(x));

                allergensCsv = string.Join(",", tags!);
            }

            var ingredientsAvailable =
                product.TryGetProperty("ingredients_text", out var ingredientsEl) &&
                ingredientsEl.ValueKind == JsonValueKind.String &&
                !string.IsNullOrWhiteSpace(ingredientsEl.GetString());

            results.Add(new
            {
                ProductCode = code,
                Name = name,
                ImageUrl = GetString("image_url") ?? "",
                Allergens = allergensCsv,
                IngredientsAvailable = ingredientsAvailable
            });
        }

        return Ok(results);
    }
}