using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    // Repository class that implements IBookingRepo to handle Booking-related database operations.
    public class BookingRepo : IBookingRepo
    {
        private readonly HotelDbContext _context; // Readonly field to store the application's database context
        public BookingRepo(HotelDbContext context)
        {
            _context = context; // Assign the provided context for database access
        }



        // Add a new Booking  to the database
        public void AddBooking(Booking booking)
        {
            _context.Booking.Add(booking); //  // Stage the new booking for insertion
            _context.SaveChanges(); // Saves changes to the database
        }

        // Get all Booking  from the database
        public List<Booking> GetAllBooking()
        {
            return _context.Booking.ToList(); // Fetches all Booking  from the database
        }


        // Get a Booking by its ID
        public Booking GetBookingById(int BookingId)
        {
            return _context.Booking.Find(BookingId); //Search for booking using primary key
        }


        // Update an existing Booking 
        public void UpdateBooking(Booking booking)
        {
            _context.Booking.Update(booking); // Mark booking as modified
            _context.SaveChanges(); // Saves changes to the database
        }


        // cancel a Booking  by its ID
        public void CancelBooking(int BookingId)
        {
            var res = _context.Booking.Find(BookingId); // Locate booking by ID
            if (res == null) return;                    // If booking not found, exit
           

            res.Status = "Cancelled"; // Mark booking status as cancelled
            _context.SaveChanges(); // Commit change to the database
            // no need to touch Room directly; availability is computed
        }

        // // Check if a booking overlaps with an existing one for the same room
        public bool ExistsOverlap(int roomId, DateTime start, DateTime end)
        {
            return _context.Booking.Any(r =>
                r.RoomId == roomId &&
                r.Status != "Cancelled" && // Ignore cancelled bookings
                start < r.CheckOutDate &&  // Starts before another booking ends
                end > r.CheckInDate // Ends after another booking starts
            );
        }








    }
}
