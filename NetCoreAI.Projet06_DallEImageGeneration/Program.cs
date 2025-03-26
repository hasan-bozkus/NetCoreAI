using Newtonsoft.Json;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "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";
        Console.Write("Example prompt: ");
        string prompt = Console.ReadLine();
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                prompt = prompt,
                n = 1,
                size = "1024x1024"
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/images/generations", content);
            string responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }
    }
}

/*
 api key: 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
 */