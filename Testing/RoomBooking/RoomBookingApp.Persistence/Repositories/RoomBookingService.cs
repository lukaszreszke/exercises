using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RoomBookingApp.Persistence.Repositories
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly RoomBookingAppDbContext _context;

        public RoomBookingService(RoomBookingAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {
            var rooms = _context.Rooms.Include(x => x.RoomBookings);
            return rooms.Where(x => x.RoomBookings.All(rb => rb.Date.Equals(date)));
        }

        public Room GetRoom(int id)
        {
            return _context.Rooms.Include(x => x.RoomBookings).DefaultIfEmpty().First(q => q.Id == id);
        }

        public IEnumerable<Room> GetRooms()
        {
            return _context.Rooms;
        }

        public void SaveBooking(RoomBooking roomBooking)
        {
            _context.Add(roomBooking);
            _context.SaveChanges();
        }
    }
}
