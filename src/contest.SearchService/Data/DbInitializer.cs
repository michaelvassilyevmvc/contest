using System.Text.Json;
using contest.SearchService.Models;
using MongoDB.Driver;
using MongoDB.Entities;

namespace contest.SearchService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("SearchDb",
            MongoClientSettings.FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));
        await DB.Index<Competition>()
            .Key(x => x.Title, KeyType.Text)
            .CreateAsync();
        var count = await DB.CountAsync<Competition>();
        if (count == 0)
        {
            Console.WriteLine("No competitions found in the database.");
            var competitionData = await File.ReadAllTextAsync("Data/competitions.json");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var competitions = JsonSerializer.Deserialize<List<Competition>>(competitionData, options);
            await DB.SaveAsync(competitions);
        }
    }
}