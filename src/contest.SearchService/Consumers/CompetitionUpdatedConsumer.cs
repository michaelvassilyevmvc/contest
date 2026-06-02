using AutoMapper;
using contest.Contracts;
using contest.SearchService.Models;
using MassTransit;
using MongoDB.Entities;

namespace contest.SearchService.Consumers;

public class CompetitionUpdatedConsumer : IConsumer<CompetitionUpdated>
{
    private readonly IMapper _mapper;

    public CompetitionUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<CompetitionUpdated> context)
    {
        Console.WriteLine("--> Consuming competition updated: " + context.Message.Id);
        var competition = _mapper.Map<Competition>(context.Message);

        var result = await DB.Update<Competition>()
            .MatchID(context.Message.Id)
            .ModifyOnly(x => new
            {
                x.Title,
                x.TicketPrice,
                x.StartDate,
                x.EndDate,
                x.SportType
            }, competition)
            .ExecuteAsync();

        if (!result.IsAcknowledged)
        {
            throw new MessageException(typeof(CompetitionUpdated), "Problem updating mongodb");
        }
    }
}