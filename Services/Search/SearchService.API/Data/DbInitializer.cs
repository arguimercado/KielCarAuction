using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.API.Entities;
using System.Text.Json;

namespace SearchService.API.Data;

public static class DbInitializer
{
    public static async Task InitDbAsync(this WebApplication app)
    {
        await DB.InitAsync(
            "SearchServiceDb",
            MongoClientSettings
                .FromConnectionString(app.Configuration.GetConnectionString("DbConnection")));
        
        await DB.Index<Item>()
            .Key(i => i.Make, KeyType.Text)
            .Key(i => i.Model, KeyType.Text)
            .Key(i => i.Color, KeyType.Ascending)
            .CreateAsync();

        var count = await DB.CountAsync<Item>();

        if(count == 0)
        {
            Console.WriteLine("No data attempt to seed");
            var itemData = await File.ReadAllTextAsync("Data/auction.json");

            var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true,
            };

            var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);

            await DB.SaveAsync(items!);
        }
    }
}
