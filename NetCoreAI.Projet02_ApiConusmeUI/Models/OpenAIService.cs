using System.Text;
using System.Text.Json;

namespace NetCoreAI20_RecipeSuggestionWithOpenAI.Models
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private const string OpenAiUrl = "https://api.openai.com/v1/chat/completions";
        private const string apiKey = "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";

        public OpenAIService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }

        public async Task<string> GetRecipeAsync(string ingredients)
        {
            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                   new { role = "system", content = "Sen profesyonel bir aşçısın. Kullanıcının elindeki malzemelere göre yemek tarifi öner." },
                   new { role = "user", content = $"Elimde şu malzemeler var: {ingredients}. Ne yapabilirim?" }
                },
                temperature = 0.7
            };
            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var response = await _httpClient.PostAsync(OpenAiUrl, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(responseBody);
            return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }
    }
}

/*
 
 apiKey: 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
 
 */