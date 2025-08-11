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
            _context.Booking.Update(booking); // Updates the booking  in the context
            _context.SaveChanges(); // Saves changes to the database
        }


        // cancel a Booking  by its ID
        public void CancelBooking(int BookingId)
        {
            var res = _context.Booking.Find(BookingId);
            if (res == null) return;

            res.Status = "Cancelled";
            _context.SaveChanges();
            // no need to touch Room directly; availability is computed
        }

        // exist Booking 
        public bool ExistsOverlap(int roomId, DateTime start, DateTime end)
        {
            return _context.Booking.Any(r =>
                r.RoomId == roomId &&
                r.Status != "Cancelled" &&
                start < r.CheckOutDate &&
                end > r.CheckInDate
            );
        }








    }
}
