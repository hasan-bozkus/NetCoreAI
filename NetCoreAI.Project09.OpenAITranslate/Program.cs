using Newtonsoft.Json;
using System.Text;
class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Lütfen çevirmek istediğiniz cümleyi giriniz: ");
        string inputText = Console.ReadLine();

        string apiKey = "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";
        string translatedText = await TranslateTextToEnglish(inputText, apiKey);

        if (!string.IsNullOrEmpty(translatedText))
        {
            Console.WriteLine();
            Console.WriteLine($"Çeviri (inglizce): {translatedText}");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Beklenmeyen bir hata oluştu.");
        }

        Console.ReadKey();
    }

    private static async Task<string> TranslateTextToEnglish(string text, string apiKey)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new object[]
                {
                    new { role = "system", content = "You are a helpful translator." },
                    new { role = "user", content = $"Please translate this text to Englis: {text}" }
                }
            };

            var jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                string responseContent = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                string translation = responseObject.choices[0].message.content;

                return translation;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                return null;
            }

        }
    }
}

/*
 api keyim: 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

 */
