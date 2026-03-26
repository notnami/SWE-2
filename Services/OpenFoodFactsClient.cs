using System.Text.Json;

namespace MyFitnessBud.Services;

public class OpenFoodFactsClient
{
    private readonly HttpClient _http;

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

    public async Task<JsonDocument?> SearchProductsAsync(string query, int pageSize = 12, CancellationToken ct = default)
    {
        var url =
            $"cgi/search.pl?search_terms={Uri.EscapeDataString(query)}" +
            $"&search_simple=1" +
            $"&action=process" +
            $"&json=1" +
            $"&page_size={pageSize}" +
            $"&fields={Uri.EscapeDataString(Fields)}";

        using var resp = await _http.GetAsync(url, ct);
        var content = await resp.Content.ReadAsStringAsync(ct);

        if (!resp.IsSuccessStatusCode)
        {
            throw new Exception($"OpenFoodFacts search failed. Status: {(int)resp.StatusCode} {resp.StatusCode}. Body: {content}");
        }

        return JsonDocument.Parse(content);
    }
}