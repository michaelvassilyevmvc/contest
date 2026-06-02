namespace contest.Contracts;

public class CompetitionUpdated
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string SportType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? TicketPrice { get; set; }
}