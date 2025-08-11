using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Services
{
    // Interface defining the contract for room-related service operations.
    // Provides methods for adding, retrieving, updating, and removing rooms.
    public interface IRoomServices
    {
        void AddNewRoom(decimal dailyRate, bool isAvailable);
        List<Room> GetAllRooms();
        Room GetRoomByID(int id);
        void RemoveRoom(int id);
        void UpdateRoom(int RoomId, bool isReserved);
        void UpdateRoom(int RoomId, int daliyRate);
    }
}