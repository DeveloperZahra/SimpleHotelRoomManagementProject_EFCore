using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    // Interface that defines the contract for guest repository operations.
    // Any class implementing this interface must handle adding, retrieving,
    // updating, and deleting guest records.
    public interface IGuestRepo
    {
        void AddGuest(Guest guest);  // Adds a new guest record to the database
        void DeleteGuest(int id); //Deletes a guest record by its unique ID
        List<Guest> GetAllGuests(); //Retrieves all guests from the database
        Guest GetGuestById(int guestId);
        void UpdateGuest(Guest guest);
    }
}