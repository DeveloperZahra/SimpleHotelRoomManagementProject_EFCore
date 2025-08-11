using SimpleHotelRoomManagementProject_EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
   // Repository class implementing IRoomRepo interface.
// Responsible for data access and management of Room entities.
    public class RoomRepo : IRoomRepo
    {
        private readonly HotelDbContext _context; // This is the database context that interacts with the database
        public RoomRepo(HotelDbContext context) // Constructor that initializes the context
        {
            _context = context;
        }

        public List<Room> GetAllRooms()
        {
            return _context.Rooms.ToList(); // Fetches all rooms from the database
        }

        public void AddRoom(Room room)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));

            _context.Rooms.Add(room); // Adds the new room to the context
            _context.SaveChanges(); // Saves changes to the database
        }


        public Room GetRoomById(int roomId)
        {
            return _context.Rooms.Find(roomId); // Finds a room by its ID
        }

        public void UpdateRoom(Room room)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));
            _context.Rooms.Update(room); // Updates the room in the context
            _context.SaveChanges(); // Saves changes to the database
        }

        // Delete a room by its ID
        public void DeleteRoom(int roomId)
        {
            var room = GetRoomById(roomId); // Retrieves the room by its ID
            if (room != null)
            {
                _context.Rooms.Remove(room); // Removes the room from the context
                _context.SaveChanges(); // Saves changes to the database
            }
        }




    }
}
