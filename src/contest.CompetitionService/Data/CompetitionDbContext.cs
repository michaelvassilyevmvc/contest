using contest.CompetitionService.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace contest.CompetitionService.Data;

public class CompetitionDbContext : DbContext
{
    public CompetitionDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Competition> Competitions { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Participant> Participants { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Venue>()
            .OwnsOne(x => x.Address);
        
        modelBuilder.Entity<Participant>()
            .OwnsOne(x => x.Address);
        
        modelBuilder.Entity<Competition>()
            .HasOne(x => x.Venue)
            .WithMany(x => x.Competitions)
            .HasForeignKey(x => x.VenueId);

        modelBuilder.Entity<Participant>()
            .HasOne(x => x.Competition)
            .WithMany(x => x.Participants)
            .HasForeignKey(x => x.CompetitionId);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
    }
}