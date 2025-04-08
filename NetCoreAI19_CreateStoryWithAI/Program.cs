using System.Text;
using System.Text.Json;

class Program
{
    private readonly static string apiKey = "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";

    static async Task Main(string[] args)
    {
        Console.Write("Hikaye Türünü Seçiniz (Macera, Korku, Bilim Kurgu, Fantastik, Komedi, Romantik): ");
        string genre = Console.ReadLine();

        Console.Write("Hikayenin Teması: ");
        string theme = Console.ReadLine();

        Console.Write("Hikayenin Seyri: ");
        string plot = Console.ReadLine();

        Console.Write("Ana karakteriniz kim: ");
        string character = Console.ReadLine();

        Console.Write("Hikayenin geçtiği yer: ");
        string setting = Console.ReadLine();

        Console.Write("Hikayenin Uzunluğu (Kısa/Orta/Uzun): ");
        string lenght = Console.ReadLine();

        string propt = $"{genre} türünde bir hikaye yaz. Hikaye {theme} temasına sahip. Hikaynin {plot} seyri bulunmakta. Baş karakterin adı {character}. Hikaye {setting} bölgesinde geçiyor. {lenght} bir hikaye olsun. Giriş, gelişme ve sonuç içermeli.";

        string story = await GenerateStory(propt);
        Console.WriteLine();
        Console.WriteLine("--- AI Tarafından Oluşturulan Hikaye --- \n");
        Console.WriteLine(story);

        static async Task<string> GenerateStory(string prompt)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "gpt-4-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are a creative story writer." },
                    new { role = "user", content = prompt }
                },
                max_tokens = 1500
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                JsonDocument doc = JsonDocument.Parse(responseContent);
                return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }

        Console.WriteLine("Hikaye Tamamlandı...");
        Console.WriteLine("Çıkmak için bir tuşa basın...");
        Console.ReadKey();

    }

}

/*
 
 apikey: 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 


Cennetten dünyaya indirilen Adem'in iki eşinden birinden olan insansı canlılar. İnsan olan Üvey kardeşlerinden intikam almak istemektedirler. Bu insansı varlıklar İnsanların nufüslarını kontrol altına alıp onlara hükmetmek istemektedirler. Günümüzde ise amaçlarına ulaşmak üzerelerdir. İnsan Olan Ana karakterin Astral seyahat ve geliştirdiği İleri teknolji Aksungur Ağır Muharebe Kanatlı Zırhlısı ile etrafında topladığı askerler ile Üvey kardeşlerinin planlarını Soylarını kurtarmak için yok etmek istemektedirler. İnsanların ve Ademin diğer eşinden olan kardeşleri ile benzer özelliklere sahip olduğunu anladığnda, bu işi bitirmek için hızla aksiyon almaktadır. Son Mücadele Nemrut Dağında Destansı bir Mücadeleye tanık oluyor. Bu işin sonunda Kazanan taraf Adem'in Tahtına geçecek.
 
 */
