using Microsoft.EntityFrameworkCore;

namespace Acme.Decorator;

public class DecoratorContext : DbContext
{
    public DecoratorContext(DbContextOptions<DecoratorContext> options) : base(options)
    {
    }

    public DbSet<Log> Log { get; set; }
}