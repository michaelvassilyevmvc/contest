using AutoMapper;
using contest.Contracts;
using contest.SearchService.Models;
using MassTransit;
using MongoDB.Entities;

namespace contest.SearchService.Consumers;

public class CompetitionCreatedConsumer : IConsumer<CompetitionCreated>
{
    private readonly IMapper _mapper;

    public CompetitionCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<CompetitionCreated> context)
    {
        Console.WriteLine("--> Consuming competition created: " + context.Message.Id);
        var competition = _mapper.Map<Competition>(context.Message);

        await competition.SaveAsync();
    }
}