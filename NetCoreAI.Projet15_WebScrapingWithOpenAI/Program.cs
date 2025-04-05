using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Text;

class Program
{
    private static readonly string apiKey = "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";

    static async Task Main(string[] args)
    {
        Console.Write("Lütfen analiz yapmak istediğiniz web safya Url'ini giriniz: ");
        string inputUrl = Console.ReadLine();

        Console.WriteLine();
        Console.WriteLine("Web sayfası içeriği: ");
        string webContent = ExtractTextFromWeb(inputUrl);
        await AnalyzeWithAI(webContent, "Web Sayfası İçeriği");

        static string ExtractTextFromWeb(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var bodyText = doc.DocumentNode.SelectSingleNode("//body")?.InnerText;
            return bodyText ?? "Sayfa içeriği okunamadı.";
        }

        static string WrapTextToConsoleWidth(string text)
        {
            int consoleWidth = Console.WindowWidth;

            // Güvenlik payı (bazı terminallerde son karakter taşabiliyor)
            int maxLineLength = consoleWidth > 5 ? consoleWidth - 2 : 78;

            StringBuilder wrapped = new StringBuilder();
            for (int i = 0; i < text.Length; i += maxLineLength)
            {
                int length = Math.Min(maxLineLength, text.Length - i);
                string part = text.Substring(i, length);

                if (i + length < text.Length)
                    wrapped.AppendLine(part + "-");
                else
                    wrapped.AppendLine(part); // Son satırda '-' koymaya gerek yok
            }

            return wrapped.ToString();
        }

        static async Task AnalyzeWithAI(string text, string sourceType)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new {role = "system", content = "Sen bir yapay zeka asistanısın. Kullanıcının gönderdiği metni analiz eder ve türkçe olarak özetlersin. Yanıtlarını sadece Türkçe ver!"},
                        new { role = "user", content = $"Analyze and summarize the following {sourceType}:\n\n{text}" }
                    }
                };

                string json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string respnonseJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(respnonseJson);

                    string aiContent = result.choices[0].message.content.ToString();
                    string wrappedText = WrapTextToConsoleWidth(aiContent);

                    Console.WriteLine($"\n AI Analizi ({sourceType}): \n {wrappedText}");
                }
                else
                {
                    Console.WriteLine("Hata: " + respnonseJson);
                }
            }
        }

        Console.ReadKey();
    }
}


/*
 
apikey: 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
 
 */
