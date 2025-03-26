using System.Net.Http.Headers;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "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";
        string audioFilePath = "ses1.mp3";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var form = new MultipartFormDataContent();

            var audioContent = new ByteArrayContent(File.ReadAllBytes(audioFilePath));
            audioContent.Headers.ContentType=MediaTypeHeaderValue.Parse("audio/mpeg");
            form.Add(audioContent, "file", Path.GetFileName(audioFilePath));
            form.Add(new StringContent("whisper-1"), "model");

            Console.WriteLine("Ses dosyası işleniyor, Lütfen bekleyiniz...");

            var response = await client.PostAsync("https://api.openai.com/v1/audio/transcriptions", form);
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Transkript: ");
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"Hata: {response.StatusCode}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }

        Console.ReadKey();
    }
}
/*
 Api Key: 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
 */