using contest.Contracts;
using contest.SearchService.Models;
using MassTransit;
using MongoDB.Entities;

namespace contest.SearchService.Consumers;

public class CompetitionDeletedConsumer : IConsumer<CompetitionDeleted>
{
    public async Task Consume(ConsumeContext<CompetitionDeleted> context)
    {
        Console.WriteLine("--> Consuming competition deleted: " + context.Message.Id);
        var result = await DB.DeleteAsync<Competition>(context.Message.Id);

        if (!result.IsAcknowledged)
        {
            throw new MessageException(typeof(CompetitionDeleted), "Problem deleting competition");
        }
    }
}