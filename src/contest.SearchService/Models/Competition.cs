using MongoDB.Entities;

namespace contest.SearchService.Models;

public class Competition: Entity
{
    public string Title { get; set; }
    public string SportType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TicketPrice { get; set; }
}