using Microsoft.EntityFrameworkCore;

namespace Acme.TemplateMethod._2;

public class DesiredDataStructure
{
    public int Id { get; set; }
    public string BandName { get; set; }
    public int Rating { get; set; }
    public List<AvailableDate> AvailableDates { get; set; }
}

public class AvailableDate
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int DesiredDataStructureId { get; set; }
    public DesiredDataStructure DesiredDataStructure { get; set; }
}

public class DesiredDataContext : DbContext
{
    public DesiredDataContext(DbContextOptions options) : base(options) {}
    public DbSet<DesiredDataStructure> Data { get; set; }
    public DbSet<AvailableDate> AvailableDates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DesiredDataStructure>().HasKey(d => d.Id);
        modelBuilder.Entity<AvailableDate>().HasKey(ad => ad.Id);

        modelBuilder.Entity<AvailableDate>()
            .HasOne(ad => ad.DesiredDataStructure)
            .WithMany(ds => ds.AvailableDates)
            .HasForeignKey(ad => ad.DesiredDataStructureId);
    }
}