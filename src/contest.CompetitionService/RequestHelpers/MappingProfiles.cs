using AutoMapper;
using contest.CompetitionService.DTOs;
using contest.CompetitionService.Entities;

namespace contest.CompetitionService.RequestHelpers;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Competition, CompetitionDto>();
        CreateMap<Participant, ParticipantDto>();
        CreateMap<Venue, VenueDto>();
    }
}