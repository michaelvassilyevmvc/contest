using AutoMapper;
using contest.CompetitionService.Data;
using contest.CompetitionService.DTOs;
using contest.CompetitionService.Entities;
using contest.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contest.CompetitionService.Controllers;

[ApiController]
[Route("api/competitions")]
public class CompetitionsController : ControllerBase
{
    private readonly CompetitionDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public CompetitionsController(CompetitionDbContext context,
        IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
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

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Competition>> CreateCompetition(CreateCompetitionDto createCompetitionDto)
    {
        var competition = _mapper.Map<Competition>(createCompetitionDto);
        _context.Competitions.Add(competition);

        // передача данных в RabbitMQ
        var competitionCreated = _mapper.Map<CompetitionDto>(competition);
        await _publishEndpoint.Publish(_mapper.Map<CompetitionCreated>(competitionCreated));

        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Couldn't save changes to the DB");


        return CreatedAtAction(nameof(GetCompetitionById), new { id = competition.Id },
            _mapper.Map<CompetitionDto>(competition));
    }

    [Authorize]
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

        // передача данных в RabbitMQ
        await _publishEndpoint.Publish(_mapper.Map<CompetitionUpdated>(competition));

        var result = await _context.SaveChangesAsync() > 0;
        if (result) return Ok();
        return BadRequest("Couldn't save changes to the DB");
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCompetition(Guid id)
    {
        var competition = await _context.Competitions.FindAsync(id);
        if (competition is null) return NotFound();

        _context.Competitions.Remove(competition);

        // передача данных в RabbitMQ
        await _publishEndpoint.Publish<CompetitionDeleted>(new
        {
            Id = competition.Id.ToString()
        });

        var result = await _context.SaveChangesAsync() > 0;
        if (result) return Ok();
        return BadRequest("Couldn't save changes to the DB");
    }
}