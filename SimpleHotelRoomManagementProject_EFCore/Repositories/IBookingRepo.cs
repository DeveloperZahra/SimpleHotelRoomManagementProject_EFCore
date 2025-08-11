using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    // Interface that defines the contract for booking repository operations.
    // Any class implementing this interface must provide concrete implementations 
    // for adding, retrieving, updating, cancelling, and checking booking overlaps.
    public interface IBookingRepo
    {
        void AddBooking(Booking booking); // Adds a new booking record to the database
        void CancelBooking(int BookingId);
        bool ExistsOverlap(int roomId, DateTime start, DateTime end);
        List<Booking> GetAllBooking();
        Booking GetBookingById(int BookingId);
        void UpdateBooking(Booking booking);
    }
}