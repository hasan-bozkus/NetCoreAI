using Newtonsoft.Json;
using System.Text;

class Program
{
    private static readonly string apiKey = "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";
    static async Task Main(string[] args)
    {
        Console.Write("Uzun metni veya makalenizi giriniz: ");
        string input;
        input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input))
        {
            Console.WriteLine();
            Console.WriteLine("Giriş olduğunuz metin AI tarafından özetleniyor...");
            Console.WriteLine();

            string shortSummary = await SummarizeText(input, "short");
            string mediumSummary = await SummarizeText(input, "medium");
            string detailedSummary = await SummarizeText(input, "detailed");

            Console.WriteLine("Özetler");
            Console.WriteLine("------------------------");
            Console.WriteLine($" ** Kısa Özet: ** {shortSummary}");
            Console.WriteLine("------------------------");
            Console.WriteLine($" ** Orta Uzunlukta Özet: ** {mediumSummary}");
            Console.WriteLine("------------------------");
            Console.WriteLine($" ** Detaylı Özet: ** {detailedSummary}");
        }

        async Task<string> SummarizeText(string text, string level)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                string instruction = level switch
                {
                    "short" => "Summarize this text in 1-2 sentences.",
                    "medium" => "Summarize this text in 3-4 sentences.",
                    "detailed" => "Summarize this text in a detailed but concise manner.",
                    _ => "Summerize this text."
                };

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                    new { role = "system", content = "You are an AI that summarize text info different levels: short, mediuam and detailed." },
                    new {role = "user", content = $"{instruction}\n\n{text}"}
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
                    Console.WriteLine($"Hata: {responseJson}");
                    return "Hata!";
                }
            }
        }

        Console.ReadKey();
    }    
}

/*
 
apikey: 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


Kış Mevsiminde Yetiştirilebilecek Sebzeler: Kış mevsimi, soğuk havalarla birlikte tarım açısından zorlu bir dönem gibi görünse de, aslında birçok sebze bu mevsimde yetiştirilmeye uygundur. Kışa dayanıklı sebzeler, serin hava koşullarında daha lezzetli ve sağlıklı ürünler verir.Lahana, kış aylarının vazgeçilmez sebzelerindendir. Soğuğa oldukça dayanıklıdır ve vitamin açısından zengindir.Pazı da soğuk havalarda yetiştirilebilen, demir ve lif bakımından önemli bir sebzedir. Ispanak, serin havayı seven yapraklı sebzelerden biridir. Kasım'dan itibaren hasat edilebilir. Pırasa, özellikle çorbalarda ve zeytinyağlı yemeklerde sıkça kullanılır ve kışın en çok tercih edilen sebzelerdendir.Karnabahar, dona dayanıklı olup kış boyunca taze olarak tüketilebilir. Brokoli, bağışıklığı güçlendiren içeriğiyle kış sofralarının aranan sebzesidir. Havuç, kış aylarında yetiştirilebilen kök sebzelerden biridir ve toprağın altında geliştiği için soğuktan etkilenmez. Kereviz, hem yaprakları hem de kökü kullanılan bir başka kış sebzesidir. Turp, soğuğa dayanıklıdır ve özellikle kış salatalarında bolca kullanılır. Soğan ve sarımsak da kışın toprakta gelişmeye devam eden, mutfakların temel sebzelerindendir. Bu sebzeler doğru ekim zamanı ve bakım ile birlikte kış boyunca verimli şekilde yetiştirilebilir. Kışlık sebzeler genellikle serin iklimi sever, bu da onların doğal olarak daha az haşereyle karşılaşmasını sağlar. Ayrıca, kış sebzeleri genellikle uzun süre saklanabilir, bu da ekonomik bir avantaj sağlar. Tohumların sonbaharda ekilmesi, sebzelerin kış aylarında hasat edilmesini mümkün kılar. Kış sebzeleri bol lifli, vitaminli ve düşük kalorili olmaları nedeniyle sağlıklı beslenmenin önemli bir parçasıdır. Bahçesi olanlar küçük bir alan ayırarak kışlık sebzeleri rahatça yetiştirebilir. Topraktan sofraya gelen doğal ürünler, kış aylarında bağışıklığı güçlendirmeye yardımcı olur. Kış tarımı, sürdürülebilir tarımın önemli bir parçasıdır. Doğru toprak hazırlığı ve don olaylarına karşı önlemlerle başarı oranı artar. Sonuç olarak, kış mevsimi de verimli ve sağlıklı ürünler yetiştirmek için fırsatlar sunar.
 
 */
