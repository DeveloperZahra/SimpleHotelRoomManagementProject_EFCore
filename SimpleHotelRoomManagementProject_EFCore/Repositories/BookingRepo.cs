using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    public  class BookingRepo
    {
        private readonly HotelDbContext _context;
        public BookingRepo(HotelDbContext context)
        {
            _context = context;
        }



        // Add a new Booking  to the database
        public void AddBooking(Booking  booking)
        {
            _context.Booking.Add(booking); // Adds the new Booking to the context
            _context.SaveChanges(); // Saves changes to the database
        }

        // Get all Booking  from the database
        public List<Booking> GetAllBooking()
        {
            return _context.booking.ToList(); // Fetches all Booking  from the database
        }


        // Get a Booking by its ID
        public Booking  GetReservationById(int BookingId)
        {
            return _context.booking.Find(BookingId); // Finds a reservation by its ID
        }

    }
}
