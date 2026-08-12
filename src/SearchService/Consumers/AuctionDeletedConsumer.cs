using MongoDB.Entities;
using MassTransit;
using Contracts;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionDeletedConsumer: IConsumer<AuctionDeleted>
{
    public async Task Consume(ConsumeContext<AuctionDeleted> context)
    {
        Console.WriteLine($"--> Consuming deleted auction {context.Message.Id}");
        var db = await DB.InitAsync("SearchDb");

        var result = await db.DeleteAsync<Item>(context.Message.Id);


        if(!result.IsAcknowledged)
        {
            throw new MessageException(typeof(AuctionDeleted),$"Problem deleting mongodb");
        }
    }
}
