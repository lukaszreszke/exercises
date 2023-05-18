using Microsoft.EntityFrameworkCore;
using Salaries.Domain;

namespace Salaries.Infra;

public class SalariesContext : DbContext
{
    public SalariesContext(DbContextOptions<SalariesContext> options) : base(options)
    {
    }

    public DbSet<Benefit> Benefits { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().OwnsOne<Money>(x => x.Salary, i =>
        {
        });
        modelBuilder.Entity<Employee>().OwnsOne<Money>(x => x.Salary, c => c.Property(x => x.Amount));
        modelBuilder.Entity<Employee>().HasMany<Benefit>();
        base.OnModelCreating(modelBuilder);
    }
}