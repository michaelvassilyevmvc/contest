using AutoMapper;
using contest.Contracts;
using contest.SearchService.Models;

namespace contest.SearchService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CompetitionCreated, Competition>();
        CreateMap<CompetitionUpdated, Competition>();
    }
}