using AutoMapper;
using contest.CompetitionService.DTOs;
using contest.CompetitionService.Entities;
using contest.Contracts;

namespace contest.CompetitionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Competition, CompetitionDto>();
        CreateMap<CreateCompetitionDto, Competition>();
        CreateMap<UpdateCompetitionDto, Competition>();

        CreateMap<Participant, ParticipantDto>();
        CreateMap<CreateParticipantDto, Participant>();
        CreateMap<UpdateParticipantDto, Participant>();

        CreateMap<Venue, VenueDto>();
        CreateMap<CreateVenueDto, Venue>();
        CreateMap<UpdateVenueDto, Venue>();

        CreateMap<CompetitionDto, CompetitionCreated>();
        CreateMap<Competition, CompetitionUpdated>();
    }
}