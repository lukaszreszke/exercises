using Microsoft.EntityFrameworkCore;

namespace Venues;

public class VenuesContext : DbContext
{
    public VenuesContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Venue> Venues { get; set; }
    public DbSet<VenueParticipant> VenueParticipants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Venue>().HasMany<VenueParticipant>();
        base.OnModelCreating(modelBuilder);
    }
}