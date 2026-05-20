using System.ComponentModel.DataAnnotations.Schema;

namespace contest.CompetitionService.Entities;

[Table("Competitions")]
public class Competition // Соревнование
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    [Column("sport_type")] public required string SportType { get; set; }
    [Column("start_date")] public DateTime StartDate { get; set; }
    [Column("end_date")] public DateTime EndDate { get; set; }
    [Column("ticket_price")] public decimal TicketPrice { get; set; }

    public Guid VenueId { get; set; }
    public Venue Venue { get; set; }

    public ICollection<Participant> Participants { get; set; } = new List<Participant>();
}