using AutoMapper;
using contest.CompetitionService.Data;
using contest.CompetitionService.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contest.CompetitionService.Controllers;

public class CompetitionsController : ControllerBase
{
    private readonly CompetitionDbContext _context;
    private readonly IMapper _mapper;

    public CompetitionsController(CompetitionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<CompetitionDto>>> GetCompetitions()
    {
        var competitions = await _context.Competitions.ToListAsync();
        return Ok(_mapper.Map<List<CompetitionDto>>(competitions));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompetitionDto>> GetCompetitionById(Guid id)
    {
        var competition = await _context.Competitions.FirstOrDefaultAsync(x => x.Id == id);
        if (competition == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<CompetitionDto>(competition));
    }
}