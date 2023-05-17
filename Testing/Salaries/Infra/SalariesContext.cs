using Microsoft.EntityFrameworkCore;
using Salaries.Domain;

namespace Salaries.Infra;

public class SalariesContext : DbContext
{
    public SalariesContext(DbContextOptions<SalariesContext> options) : base(options)
    {
    }

    public DbSet<Salary> Salaries { get; set; }
    public DbSet<Benefit> Benefits { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasOne<Salary>();
        modelBuilder.Entity<Employee>().HasMany<Benefit>();
        base.OnModelCreating(modelBuilder);
    }
}