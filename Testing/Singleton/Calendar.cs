using Microsoft.EntityFrameworkCore;
namespace Singleton
{
    public class Calendar
    {
        private readonly ApplicationDbContext _dbContext;

        public Calendar(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            MessageBus.Subscribe(message =>
            {
                if (message.StartsWith("add_event:"))
                {
                    string[] parts = message.Split(':');
                    string title = parts[1];
                    DateTime start = DateTime.Parse(parts[2]);
                    DateTime end = DateTime.Parse(parts[3]);

                    if (IsEventValid(title, start, end))
                    {
                        AddEvent(title, start, end);
                    }
                    else
                    {
                        Console.WriteLine("Cannot add event - it overlaps with an existing event.");
                    }
                }
                else if (message.StartsWith("remove_event:"))
                {
                    string[] parts = message.Split(':');
                    string title = parts[1];

                    RemoveEvent(title);
                }
            });
        }

        private bool IsEventValid(string title, DateTime start, DateTime end)
        {
            if (start >= end)
            {
                return false; // End time must be after start time
            }

            var conflictingEvents = _dbContext.CalendarEvents
                .Where(e => e.Title != title && e.StartTime < end && e.EndTime > start)
                .ToList();

            return conflictingEvents.Count == 0; // No overlapping events found
        }

        private void AddEvent(string title, DateTime start, DateTime end)
        {
            var newEvent = new CalendarEvent
            {
                Title = title,
                StartTime = start,
                EndTime = end
            };

            _dbContext.CalendarEvents.Add(newEvent);
            _dbContext.SaveChanges();

            Console.WriteLine($"Added event: {title}, {start} - {end}");
        }

        private void RemoveEvent(string title)
        {
            var eventToRemove = _dbContext.CalendarEvents.FirstOrDefault(e => e.Title == title);
            if (eventToRemove != null)
            {
                _dbContext.CalendarEvents.Remove(eventToRemove);
                _dbContext.SaveChanges();

                Console.WriteLine($"Removed event: {title}");
            }
        }
    }
}