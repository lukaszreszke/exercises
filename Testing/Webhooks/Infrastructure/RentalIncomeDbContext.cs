using Microsoft.EntityFrameworkCore;

namespace Webhooks.Infrastructure;

public class RentalIncomeDbContext : DbContext
{
    public RentalIncomeDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<RentalIncome> RentalIncomes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RentalIncome>()
            .HasKey(x => x.Id);
        base.OnModelCreating(modelBuilder);
    }
}

public class RentalIncome
{
    private RentalIncome()
    {
        
    }
    
    public RentalIncome(decimal value, DateTime since, DateTime until, string externalId)
    {
        Value = value;
        Since = since;
        Until = until;
        ExternalId = externalId;
    }

    public int Id { get; }
    public decimal Value { get; set; }
    public DateTime Since { get; set;  }
    public DateTime Until { get; set; }
    public string ExternalId { get; set; }
}