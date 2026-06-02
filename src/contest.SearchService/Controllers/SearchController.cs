using contest.SearchService.Models;
using contest.SearchService.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;

namespace contest.SearchService.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Search([FromQuery] SearchParams searchParams)
    {
        var query = DB.PagedSearch<Competition, Competition>();
        query.Sort(x => x.Ascending(a => a.Title));

        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(MongoDB.Entities.Search.Full, searchParams.SearchTerm)
                .SortByTextScore();
        }

        query = searchParams.OrderBy switch
        {
            "byStart" => query.Sort(x => x.Ascending(a => a.StartDate)),
            "byEnd" => query.Sort(x => x.Descending(a => a.EndDate)),
            _ => query.Sort(x => x.Ascending(a => a.Title))
        };

        query = searchParams.FilterBy switch
        {
            "tennis" => query.Match(x => x.SportType == "Tennis"),
            _ => query.Match(x => true)
        };
        
        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);
        
        var result = await query.ExecuteAsync();

        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}