using Microsoft.EntityFrameworkCore;

namespace Singleton
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CalendarEvent> CalendarEvents { get; set; }
    }
}