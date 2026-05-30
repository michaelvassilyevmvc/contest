using contest.SearchService.Models;
using MongoDB.Entities;

namespace contest.SearchService.Services;

public class CompetitionSvcHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CompetitionSvcHttpClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<List<Competition>> GetCompetitionsForSearchAsync()
    {
        var lastUpdated = await DB.Find<Competition, string>()
            .Sort(x => x.Descending(a => a.EndDate))
            .Project(x => x.EndDate.ToString())
            .ExecuteFirstAsync();
        return await _httpClient.GetFromJsonAsync<List<Competition>>(_configuration["CompetitionServiceUrl"]
                                                                     + "/api/competitions?date=" + lastUpdated);
    }
}