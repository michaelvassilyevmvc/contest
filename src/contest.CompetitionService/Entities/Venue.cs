using System.ComponentModel.DataAnnotations.Schema;

namespace contest.CompetitionService.Entities;

[Table("Venues")]
public class Venue // Места проведения
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public Address Address { get; set; }= new();
    public int Capacity { get; set; } // вместимость
    [Column("venue_type")] public VenueType VenueType { get; set; } = VenueType.Building;

    public ICollection<Competition> Competitions { get; set; }= new List<Competition>();
}