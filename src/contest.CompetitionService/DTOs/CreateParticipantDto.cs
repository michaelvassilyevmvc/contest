using contest.CompetitionService.Entities;

namespace contest.CompetitionService.DTOs;

public class CreateParticipantDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Address Address { get; set; }
}