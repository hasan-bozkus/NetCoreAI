using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

class Prgram
{
    private static readonly string apiKey = "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";
    static async Task Main(string[] args)
    {
        Console.Write("Lütfen metni giriniz: ");
        string input;
        input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine();
            Console.WriteLine("Duygu analizi yapılıyor...");
            Console.WriteLine();
            string sentiment = await AnalyzeSentiment(input);

            Console.WriteLine($"Sonuç: {sentiment}");
        }

        static async Task<string> AnalyzeSentiment(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "You are an AI that analyzes sentiment. You categorize text as Positive, Negative or Neutral." },
                        new { role = "user", content = $"Analyze the sentiment of this text: \"{text}\" and return only Positive, Negative or Neutral" }
                    }
                };

                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                string responseJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    return result.choices[0].message.content.ToString();
                }
                else
                {
                    Console.WriteLine($"Bir hata oluştu: {responseJson}");
                    return "Hata";
                }
            }
        }
    }
}

/*
 
apikey: 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
 
 */
