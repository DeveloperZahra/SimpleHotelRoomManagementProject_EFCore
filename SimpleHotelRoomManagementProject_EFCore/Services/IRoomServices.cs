using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Services
{
    // Interface defining the contract for room-related service operations.
    // Provides methods for adding, retrieving, updating, and removing rooms.
    public interface IRoomServices
    {
        void AddNewRoom(decimal dailyRate, bool isAvailable);  // Adds a new room with the specified daily rate and availability 
        List<Room> GetAllRooms(); //Retrieves a list of all rooms
        Room GetRoomByID(int id); // Retrieves a room by its unique ID.
        void RemoveRoom(int id); //Removes a room by its unique ID.
        void UpdateRoom(int RoomId, bool isReserved); //Updates the reservation status of a room identified by RoomId.
        void UpdateRoom(int RoomId, int daliyRate);// Updates the daily rate of a room identified by RoomId.
                                                   // Note: 'daliyRate' has a typo; should be 'dailyRate
    }
}