
namespace SimpleHotelRoomManagementProject_EFCore.Services
{
    public interface IBookingServices
    {
        (bool Ok, string Message, int? BookingId) AddNewBooking(int nights, DateTime checkInDate, int guestId, int roomId);
    }
}