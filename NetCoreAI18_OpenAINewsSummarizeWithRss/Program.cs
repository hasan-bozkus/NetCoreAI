using System.Text;
using System.Text.Json;
using System.Xml.Linq;

class Program
{
    private readonly static string apiKey = "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";
    private readonly static string rssFeedUrl = "https://www.sabah.com.tr/rss/anasayfa.xml";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Haberler Sistemden Alınıyor...");
        List<string> articles = await FetchLatestNews(10);

        foreach(var article in articles)
        {
            Console.WriteLine("Haberler özeti oluşturuluyor...");
            string summary = await SummarizeArticle(article);
            Console.WriteLine("--- AI tarafından özetlenen haber --- \n");
            Console.WriteLine(summary);
            Console.WriteLine("----------------------------------------------- \n");
        }

        Console.ReadKey();
    }

    static async Task<List<string>> FetchLatestNews(int count)
    {
        var client = new HttpClient();
        string rssContent = await client.GetStringAsync(rssFeedUrl);
        XDocument doc = XDocument.Parse(rssContent);
        var channel = doc.Descendants("channel");
        var items = channel?.Elements("item").Take(count);

        List<string> articles = items.Select(item =>
        {
            string title = item.Element("title")?.Value ?? "";
            string description = item.Element("description")?.Value ?? "";
            return $"{title}. {description}";
        }).ToList();

        return articles;
    }

    static async Task<string> SummarizeArticle(string articleText)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        var requestBody = new
        {
            model = "gpt-4-turbo",
            messages = new[]
            {
                new { role = "system", content = "You are an expert news summarizer." },
                new { role = "user", content = "Bu haberi 3 cümlede özetle: " + articleText }
            },
            max_tokens = 500
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);

        string responseContent = await response.Content.ReadAsStringAsync();
        JsonDocument doc = JsonDocument.Parse(responseContent);
        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
    }
}

/*
 
 apikey: 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
 
 */
