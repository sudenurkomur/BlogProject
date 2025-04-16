using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class GeminiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["Gemini:ApiKey"]; // Load from appsettings.json
    }

    public async Task<string> FixGrammarAsync(string text)
    {
        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = $"Lütfen aşağıdaki metindeki yazım ve dil bilgisi hatalarını düzelt ve sadece düzelttiğin texti yaz: {text}" }
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Use the same URL structure as the cURL with the API key as a query parameter
        var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}";

        // Clear any default headers to avoid conflicts
        _httpClient.DefaultRequestHeaders.Clear();

        try
        {
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var resultJson = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(resultJson);
            var textResult = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return textResult;
        }
        catch (HttpRequestException ex)
        {
            // Handle HTTP errors
            throw new Exception("API request failed", ex);
        }
        catch (JsonException ex)
        {
            // Handle JSON parsing errors
            throw new Exception("Failed to parse API response", ex);
        }
    }
}