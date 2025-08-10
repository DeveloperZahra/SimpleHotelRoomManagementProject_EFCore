namespace SimpleHotelRoomManagementProject_EFCore.Services
{
    public interface IGuestServices
    {
        void AddNewGuest(int guestId, string guestname, string guestemail, string GuestPhoneNo);
        void DeleteGuest(int id);
        void GetAllGuest();
        void GetGuestById(int guestId);
        void UpdateGuest(int GuestID, string phone);
    }
}