using AutoMapper;
using contest.CompetitionService.Data;
using contest.CompetitionService.DTOs;
using contest.CompetitionService.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contest.CompetitionService.Controllers;

[ApiController]
[Route("api/venues")]
public class VenuesController : ControllerBase
{
    private readonly CompetitionDbContext _context;
    private readonly IMapper _mapper;

    public VenuesController(CompetitionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<VenueDto>>> GetVenues()
    {
        var venues = await _context.Venues.ToListAsync();
        return Ok(_mapper.Map<List<VenueDto>>(venues));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VenueDto>> GetVenueById(Guid id)
    {
        var venue = await _context.Venues.FirstOrDefaultAsync(x => x.Id == id);
        if (venue is null) return NotFound();
        return Ok(_mapper.Map<VenueDto>(venue));
    }

    [HttpPost]
    public async Task<ActionResult<Venue>> CreateVenue(CreateVenueDto createVenueDto)
    {
        var venue = _mapper.Map<Venue>(createVenueDto);
        _context.Venues.Add(venue);
        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Couldn't save changes to the DB");
        return CreatedAtAction(nameof(GetVenueById), new { id = venue.Id },
            _mapper.Map<VenueDto>(venue));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<VenueDto>> UpdateVenue(Guid id, UpdateVenueDto updateVenueDto)
    {
        var venue = await _context.Venues.FirstOrDefaultAsync(x => x.Id == id);
        if (venue is null) return NotFound();

        venue.Name = updateVenueDto.Name ?? venue.Name;
        venue.Address = updateVenueDto.Address ?? venue.Address;
        venue.Capacity = updateVenueDto.Capacity ?? venue.Capacity;
        venue.VenueType = updateVenueDto.VenueType ?? venue.VenueType;

        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Couldn't save changes to the DB");
        return Ok(_mapper.Map<VenueDto>(venue));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteVenue(Guid id)
    {
        var venue = await _context.Venues.FindAsync(id);
        if (venue is null) return NotFound();

        _context.Venues.Remove(venue);
        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return BadRequest("Couldn't save changes to the DB");
        return Ok();
    }
}