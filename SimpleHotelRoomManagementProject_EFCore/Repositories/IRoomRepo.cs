using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    // Interface defining contract for room repository operations.
    // Classes implementing this interface must provide CRUD functionality for Room entities.
    public interface IRoomRepo
    {
        void AddRoom(Room room);
        void DeleteRoom(int roomId);
        List<Room> GetAllRooms();
        Room GetRoomById(int roomId);
        void UpdateRoom(Room room);
    }
}