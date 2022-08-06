using Microsoft.EntityFrameworkCore;

namespace DoctorsAppointment;

public class CalendarDbContext : DbContext 
{
    public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Calendar> Calendars { get; set; }
}