using System.Data;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var ApiKey = "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";
        using var httpClinet = new HttpClient();
        httpClinet.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

        Console.WriteLine("Lütfen sorunuzu yazınız: (örnek: 'Mardinde hava kaç derece')");

        while (true)
        {
            var prompt = Console.ReadLine();


            var messages = new[]
            {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = prompt}
            };

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = messages,
                max_tokens = 500
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var respnose = await httpClinet.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var responseString = await respnose.Content.ReadAsStringAsync();
                if (respnose.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                    var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                    Console.WriteLine("Open AI'ın Cevabı: ");
                    Console.WriteLine(answer);
                }
                else
                {
                    Console.WriteLine($"Bir hata oluştu: {respnose.StatusCode}");
                    Console.WriteLine(responseString);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }

            Console.WriteLine();

        }
    }
}

/*
 Authorization Bearer 123456abcde

benim keyim:
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

 */