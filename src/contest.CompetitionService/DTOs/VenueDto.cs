using System.ComponentModel.DataAnnotations.Schema;
using contest.CompetitionService.Entities;

namespace contest.CompetitionService.DTOs;

public class VenueDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Address Address { get; set; }
    public int Capacity { get; set; } 
    public VenueType VenueType { get; set; }
}