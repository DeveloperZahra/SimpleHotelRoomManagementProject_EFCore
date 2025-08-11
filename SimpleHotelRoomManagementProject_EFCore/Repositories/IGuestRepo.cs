using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    public interface IGuestRepo
    {
        void AddGuest(Guest guest);
        void DeleteGuest(int id);
        List<Guest> GetAllGuests();
        Guest GetGuestById(int guestId);
        void UpdateGuest(Guest guest);
    }
}