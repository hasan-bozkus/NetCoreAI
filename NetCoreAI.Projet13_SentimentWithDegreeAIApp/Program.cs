using Newtonsoft.Json;
using System.Text;

class Program
{
    private static readonly string apiKey = "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";

    static async Task Main(string[] args)
    {
        Console.Write("Bir metin giriniz: ");
        string input;
        input = Console.ReadLine();
        Console.WriteLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Gelişmiş duygu analizi yapılıyor...");
            string sentiment = await AdvancedSentimentalAnlysis(input);
            Console.WriteLine();
            Console.WriteLine($"\n Sonuç: \n {sentiment}");
        }

        static async Task<string> AdvancedSentimentalAnlysis(string text)
        {
            using (HttpClient clinet = new HttpClient())
            {
                clinet.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role="system", content = "You are an advanced AI taht analyzes emotions in text. You response must bu in JSON format. Identiyf the sentiment scores(0-100%) for the following emotinos: Joy, Sadness, Anger, Fear, Surprise, and Neutral." },
                        new { role = "user", content = $"Analyzes this text : \"{text}\" and return a JSON object with percentages for each emotions." },
                    }
                };

                string json = JsonConvert.SerializeObject(requestBody);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await clinet.PostAsync("https://api.openai.com/v1/chat/completions", content);

                string responseJson = await response.Content.ReadAsStringAsync();
                if(response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseJson);
                    string analyzis = result.choices[0].message.content.ToString();
                    return analyzis;
                }
                else
                {
                    Console.WriteLine($"Bir hata oluştu: {responseJson}");
                    return "Hata!";
                }
            }
        }

    }
}

/*
 
api key: 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
 
 */
