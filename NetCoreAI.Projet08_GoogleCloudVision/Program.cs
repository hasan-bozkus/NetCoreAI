using Google.Cloud.Vision.V1;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Resim yolunu giriniz: ");
        string imagePath = Console.ReadLine();
        Console.WriteLine();

        string credentialPath = @"D:\Sertifikalarım\Başlanmış ve Devam Eden Projeler\C# .Net ile Yapay Zeka Entegrasyonları\NetCoreAI\NetCoreAI.Projet08_GoogleCloudVision\myaiapiproject-41b24cc1e3ab.json";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

        try
        {
            var client = ImageAnnotatorClient.Create();

            var image = Image.FromFile(imagePath);
            var response = client.DetectText(image);
            Console.WriteLine("Resimdeki Metin: ");
            Console.WriteLine();
            foreach (var annotation in response)
            {
                if (!string.IsNullOrEmpty(annotation.Description))
                {
                    Console.WriteLine(annotation.Description);
                }
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Bir hata oluştu {ex.Message}");
        }
    }
}