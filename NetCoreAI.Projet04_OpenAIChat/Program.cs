
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var ApiKey = "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";

        Console.WriteLine("Lütfen sorunuzu yazınız: (örnek: 'Mardinde hava kaç derece')");

        var prompt = Console.ReadLine();
        using var httpClinet = new HttpClient();
        httpClinet.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");

        var requestBody = new
        {
            model = "gbt-3.5-turbo",
            messages = new[]
            {
                new {role= "system", content = "You are a helpful assistant."},
                new {role= "user", content = prompt}
            },
            max_tokens = 500000000
        };

        var json=JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var respnose = await httpClinet.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await respnose.Content.ReadAsStringAsync();
            if(respnose.IsSuccessStatusCode)
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

    }
}

/*
 Authorization Bearer 123456abcde

benim keyim:
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

 */