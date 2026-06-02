namespace contest.SearchService.RequestHelpers;

public class SearchParams
{
    public string? SportType { get; set; } = string.Empty;
    public string? SearchTerm { get; set; } = string.Empty;
    public string? FilterBy { get; set; }
    public string? OrderBy { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 4;
}