using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    // Interface that defines the contract for guest repository operations.
    // Any class implementing this interface must handle adding, retrieving,
    // updating, and deleting guest records.
    public interface IGuestRepo
    {
        void AddGuest(Guest guest);  // Adds a new guest record to the database
        void DeleteGuest(int id);
        List<Guest> GetAllGuests();
        Guest GetGuestById(int guestId);
        void UpdateGuest(Guest guest);
    }
}