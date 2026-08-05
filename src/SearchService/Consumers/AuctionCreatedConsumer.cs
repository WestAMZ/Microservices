using AutoMapper;
using Contracts;
using MassTransit;
using MassTransit.Initializers;
using MongoDB.Entities;
using SearchService.Models;


namespace SearchService.Consumers;

// Note: Consumer at the end is part of naming convention of MassTransit
public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
{
    private readonly IMapper _mapper;

    public AuctionCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
        Console.WriteLine("--> Consuming auction created: " + context.Message.Id);

        var db = await DB.InitAsync("SearchDb");
        var item = _mapper.Map<Item>(context.Message);

        // Custom validation logic can be added here if needed, eg: we are not allowing items with Model == Foo
        if (item.Model == "Foo")
        {
            throw new ArgumentException("We cannot sell car with name of Foo");
        }

        await db.SaveAsync(item);
    }
}
