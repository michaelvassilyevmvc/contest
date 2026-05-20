using contest.CompetitionService.Entities;

namespace contest.CompetitionService.DTOs;

public class UpdateVenueDto
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public int Capacity { get; set; }
    public VenueType VenueType { get; set; }
}