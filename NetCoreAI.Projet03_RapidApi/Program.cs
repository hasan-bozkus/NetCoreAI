using NetCoreAI.Projet03_RapidApi.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
var client = new HttpClient();
List<ApiSeriesViewModel> apiSeriesViewModels = new List<ApiSeriesViewModel>();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/series/"),
    Headers =
    {
        { "x-rapidapi-key", "0c961a5194msh8f9b8f4001388a5p1c0833jsn4e305cb370d8" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    apiSeriesViewModels = JsonConvert.DeserializeObject<List<ApiSeriesViewModel>>(body);
   foreach(var series in apiSeriesViewModels)
    {
        Console.WriteLine(series.rank + "-" + series.title + " - Film Puanı: " + series.rating + " - Yapım Yılı: " + series.year);
    }
}
Console.ReadLine();