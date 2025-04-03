using Newtonsoft.Json;
using System.Text;
using System.Globalization;

class Program
{
    private static string apiKey = "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";

    static string apikey;
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.Write("Metni Girinizi: ");
        string input;
        input = Console.ReadLine();



        if (!string.IsNullOrEmpty(input))
        {
            string fixedInput = ReverseTextForRTL(input);
            Console.WriteLine("Ses dosyası oluşturuluyor...");
            await GenerateSpeech(fixedInput );
            Console.WriteLine("Ses dosyası 'output.mp3' olarak kaydedildi!");
            System.Diagnostics.Process.Start("explorer.exe", "output.mp3");
        }

        static string ReverseTextForRTL(string text)
        {
            // Arapça veya İbranice veya farklı bir RTL dili içerip içermediğini kontrol et
            if (ContainsRTLCharacters(text))
            {
                char[] array = text.ToCharArray();
                Array.Reverse(array);
                return new string(array);
            }
            return text; // Eğer RTL değilse olduğu gibi bırak
        }

        static bool ContainsRTLCharacters(string text)
        {
            foreach (char c in text)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category == UnicodeCategory.OtherLetter) // RTL diller genellikle "OtherLetter" kategorisine girer
                {
                    return true;
                }
            }
            return false;
        }


        static async Task GenerateSpeech(string text)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "tts-1-hd",
                    input = text,
                    voice = "alloy"
                };

               
                try
                {
                    string json = JsonConvert.SerializeObject(requestBody);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/audio/speech", content);

                    if (response.IsSuccessStatusCode)
                    {
                        byte[] audioBytes = await response.Content.ReadAsByteArrayAsync();
                        await File.WriteAllBytesAsync("output.mp3", audioBytes);

                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Hata: {errorResponse}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }
            }
        }
    }
}

/*
 
api key:   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

 */

/*
 * İlk olarak ReverseTextForRTL metodu çağrılır. Bu fonksiyon, metnin RTL (sağdan sola) dillerden biri olup olmadığını kontrol eder. 
 * Eğer öyleyse, ContainsRTLCharacters metodu çağırılır, eğer metod RTL formatında ise OtherLetter kategorsine girdiği doğrulanır ve 
 * metni ters çevirir. Ardından GenerateSpeech fonksiyonu çağrılır ve metin ses dosyasına dönüştürülür.
 */
