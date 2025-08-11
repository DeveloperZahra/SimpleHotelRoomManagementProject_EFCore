using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    // Interface defining contract for room repository operations.
    // Classes implementing this interface must provide CRUD functionality for Room entities.
    public interface IRoomRepo
    {
        void AddRoom(Room room); //Adds a new Room entity to the database.
        void DeleteRoom(int roomId); //Deletes an existing Room from the database by its unique ID.
        List<Room> GetAllRooms();
        Room GetRoomById(int roomId);
        void UpdateRoom(Room room);
    }
}