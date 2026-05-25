using AutoMapper;
using contest.CompetitionService.Data;
using contest.CompetitionService.DTOs;
using contest.CompetitionService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contest.CompetitionService.Controllers;

[ApiController]
[Route("api/competitions")]
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

    [HttpPost]
    public async Task<ActionResult<Competition>> CreateCompetition(CreateCompetitionDto createCompetitionDto)
    {
        var competition = _mapper.Map<Competition>(createCompetitionDto);
        _context.Competitions.Add(competition);

        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Couldn't save changes to the DB");
        return CreatedAtAction(nameof(GetCompetitionById), new { id = competition.Id },
            _mapper.Map<CompetitionDto>(competition));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CompetitionDto>> UpdateCompetition(Guid id,
        UpdateCompetitionDto updateCompetitionDto)
    {
        var competition = await _context.Competitions.FirstOrDefaultAsync(x => x.Id == id);
        if (competition is null) return NotFound();

        competition.Title = updateCompetitionDto.Title ?? competition.Title;
        competition.SportType = updateCompetitionDto.SportType ?? competition.SportType;
        competition.StartDate = updateCompetitionDto.StartDate ?? competition.StartDate;
        competition.EndDate = updateCompetitionDto.EndDate ?? competition.EndDate;
        competition.TicketPrice = updateCompetitionDto.TicketPrice ?? competition.TicketPrice;

        var result = await _context.SaveChangesAsync() > 0;
        if (result) return Ok();
        return BadRequest("Couldn't save changes to the DB");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCompetition(Guid id)
    {
        var competition = await _context.Competitions.FindAsync(id);
        if (competition is null) return NotFound();

        _context.Competitions.Remove(competition);
        var result = await _context.SaveChangesAsync() > 0;
        if (result) return Ok();
        return BadRequest("Couldn't save changes to the DB");
    }
}