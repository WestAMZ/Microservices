using MongoDB.Entities;
using AutoMapper;
using Contracts;
using MassTransit;
using SearchService.Models;

namespace SearchService.Consumers;

public class AuctionUpdatedConsumer: IConsumer<AuctionUpdated>
{
    private readonly IMapper _mapper;

    public AuctionUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        Console.WriteLine($"--> Consuming updated auction {context.Message.Id}");
        var item = _mapper.Map<Item>(context.Message);

        var db = await DB.InitAsync("SearchDb");

        var result = await db.Update<Item>()
            .Match(x => x.ID == context.Message.Id)
            .ModifyOnly(x => new {
                x.Color, 
                x.Make, 
                x.Model, 
                x.Mileage, 
            }, item)
            .ExecuteAsync();

        if(!result.IsAcknowledged)
        {
            throw new MessageException(typeof(AuctionUpdated),$"Problem updating mongodb");
        }
    }
}
