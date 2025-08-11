namespace SimpleHotelRoomManagementProject_EFCore.Services
{

    // Interface that defines the contract for guest service operations.
    // Any class implementing this interface will handle business logic
    // related to adding, retrieving, updating, and deleting guest records.
    public interface IGuestServices
    {
        void AddNewGuest(int guestId, string guestname, string guestemail, string GuestPhoneNo); //Adds a new guest with the provided ID, name, email, and phone number
        void DeleteGuest(int id); //Deletes a guest by their unique ID
        void GetAllGuest(); //Retrieves and displays all guests
        void GetGuestById(int guestId); //Retrieves and displays a guest by their unique ID
        void UpdateGuest(int GuestID, string phone);
    }
}