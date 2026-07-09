using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Data;
public class DbInitializer
{
    public static async Task InitDb(WebApplicationBuilder app)
    {
        var db = await DB.InitAsync("SearchDb", MongoClientSettings.
            FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        await db.Index<Item>() 
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();
        
        var count = await db.CountAsync<Item>();

        if (count == 0)
        {
            Console.WriteLine("No data - will attmept to seed");
            var itemData = await File.ReadAllTextAsync("Data/auctions.json");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);

            await db.SaveAsync(items);
        }
    }
}
