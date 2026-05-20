using AutoMapper;
using contest.CompetitionService.Data;
using contest.CompetitionService.DTOs;
using contest.CompetitionService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contest.CompetitionService.Controllers;

[ApiController]
[Route("api/participants")]
public class ParticipantsController : ControllerBase
{
    private readonly CompetitionDbContext _context;
    private readonly IMapper _mapper;

    public ParticipantsController(CompetitionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ParticipantDto>>> GetParticipants()
    {
        var participants = await _context.Participants.ToListAsync();
        return Ok(_mapper.Map<List<ParticipantDto>>(participants));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParticipantDto>> GetParticipant(Guid id)
    {
        var participant = await _context.Participants.FirstOrDefaultAsync(x => x.Id == id);
        if (participant is null) return NotFound();
        return Ok(_mapper.Map<ParticipantDto>(participant));
    }

    [HttpPost]
    public async Task<ActionResult<Participant>> CreateParticipant(CreateParticipantDto createParticipantDto)
    {
        var participant = _mapper.Map<Participant>(createParticipantDto);
        _context.Participants.Add(participant);
        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Couldn't save changes to the DB");
        return CreatedAtAction(nameof(GetParticipant), new { id = participant.Id },
            _mapper.Map<ParticipantDto>(participant));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ParticipantDto>> UpdateParticipant(Guid id,
        UpdateParticipantDto updateParticipantDto)
    {
        var participant = await _context.Participants.FirstOrDefaultAsync(x => x.Id == id);
        if (participant is null) return NotFound();

        participant.FirstName = updateParticipantDto.FirstName ?? participant.FirstName;
        participant.LastName = updateParticipantDto.LastName ?? participant.LastName;
        participant.BirthDate = updateParticipantDto.BirthDate ?? participant.BirthDate;
        participant.Address = updateParticipantDto.Address ?? participant.Address;

        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Couldn't save changes to the DB");
        return Ok(_mapper.Map<ParticipantDto>(participant));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteParticipant(Guid id)
    {
        var participant = await _context.Participants.FindAsync(id);
        if (participant is null) return NotFound();

        _context.Participants.Remove(participant);
        var result = await _context.SaveChangesAsync() > 0;
        if (result) return Ok();
        return BadRequest("Couldn't save changes to the DB");
    }
}