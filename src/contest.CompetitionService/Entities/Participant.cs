using System.ComponentModel.DataAnnotations.Schema;

namespace contest.CompetitionService.Entities;

[Table("Participants")]
public class Participant // Участники
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Column("first_name")] public required string FirstName { get; set; }
    [Column("last_name")] public required string LastName { get; set; }
    [Column("birth_date")] public DateOnly? BirthDate { get; set; }
    public Address Address { get; set; } = new();

    public Guid CompetitionId { get; set; }
    public Competition Competition { get; set; } = null!;
}