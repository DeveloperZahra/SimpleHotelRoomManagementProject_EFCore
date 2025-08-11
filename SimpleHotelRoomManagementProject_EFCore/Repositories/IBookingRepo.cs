using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    // Interface that defines the contract for booking repository operations.
    // Any class implementing this interface must provide concrete implementations 
    // for adding, retrieving, updating, cancelling, and checking booking overlaps.
    public interface IBookingRepo
    {
        void AddBooking(Booking booking); // Adds a new booking record to the database
        void CancelBooking(int BookingId);  // Cancels a booking by its unique ID
        bool ExistsOverlap(int roomId, DateTime start, DateTime end);  // Checks if there is an overlapping booking for the same room within a given date range
        List<Booking> GetAllBooking();     // Retrieves all bookings from the database
        Booking GetBookingById(int BookingId); // Retrieves a booking by its unique ID
        void UpdateBooking(Booking booking);
    }
}