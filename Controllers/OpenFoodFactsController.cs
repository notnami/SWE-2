using Microsoft.AspNetCore.Mvc;
using MyFitnessBud.Services;
using System.Text.Json;

namespace MyFitnessBud.Controllers
{
    [ApiController]
    [Route("api/off")]
    public class OpenFoodFactsController : ControllerBase
    {
        private readonly OpenFoodFactsClient _off;

        public OpenFoodFactsController(OpenFoodFactsClient off)
        {
            _off = off;
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

            if (!root.TryGetProperty("products", out var products) ||
                products.ValueKind != JsonValueKind.Array)
            {
                return Ok(Array.Empty<object>());
            }

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
}