using System.Text.Json;

namespace MyFitnessBud.Services;

public class OpenFoodFactsClient
{
    private readonly HttpClient _http;

    // Request only what you need
    private const string Fields = "code,product_name,image_url,allergens_tags,ingredients_text,url";

    public OpenFoodFactsClient(HttpClient http) => _http = http;

    public async Task<JsonDocument?> GetProductAsync(string barcode, CancellationToken ct = default)
    {
        var url = $"api/v2/product/{Uri.EscapeDataString(barcode)}?fields={Fields}";
        using var resp = await _http.GetAsync(url, ct);
        if (!resp.IsSuccessStatusCode) return null;

        await using var stream = await resp.Content.ReadAsStreamAsync(ct);
        return await JsonDocument.ParseAsync(stream, cancellationToken: ct);
    }
}