namespace SimpleHotelRoomManagementProject_EFCore.Services
{

    // Interface that defines the contract for guest service operations.
    // Any class implementing this interface will handle business logic
    // related to adding, retrieving, updating, and deleting guest records.
    public interface IGuestServices
    {
        void AddNewGuest(int guestId, string guestname, string guestemail, string GuestPhoneNo);
        void DeleteGuest(int id);
        void GetAllGuest();
        void GetGuestById(int guestId);
        void UpdateGuest(int GuestID, string phone);
    }
}