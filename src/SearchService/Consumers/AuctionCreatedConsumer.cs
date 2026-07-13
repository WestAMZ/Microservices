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

        await db.SaveAsync(item);
    }
}
