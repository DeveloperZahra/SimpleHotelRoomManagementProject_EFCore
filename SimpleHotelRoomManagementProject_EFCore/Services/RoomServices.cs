using SimpleHotelRoomManagementProject_EFCore.Models;
using SimpleHotelRoomManagementProject_EFCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Services
{
    // Service class implementing IRoomServices interface.
    // Contains business logic related to managing rooms,
    // including adding, retrieving, updating, and removing rooms
    public class RoomServices : IRoomServices
    {
        private readonly IRoomRepo _roomRepository; //Repository dependency for room data access
        public RoomServices(IRoomRepo roomRepository)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }


        public void AddNewRoom(decimal dailyRate, bool isAvailable)
        {
            // Create a new Room object
            var room = new Room
            {
                DailyRate = dailyRate,
                IsReserved = isAvailable
            };
            _roomRepository.AddRoom(room);
        }

        // Get All Rooms in the database through the repository
        public List<Room> GetAllRooms()
        {
            return _roomRepository.GetAllRooms();
        }

        // Get Room by id 
        public Room GetRoomByID(int id)
        {
            return _roomRepository.GetRoomById(id);
        }

        // Remove Room
        public void RemoveRoom(int id)
        {
            _roomRepository.DeleteRoom(id);
        }

        // update Reserved room
        public void UpdateRoom(int RoomId, bool isReserved)
        {
            var existingroom = _roomRepository.GetRoomById(RoomId);
            if (existingroom != null)
            {
                existingroom.IsReserved = isReserved;

                _roomRepository.UpdateRoom(existingroom);
            }
        }

        // update daliyRate of room 
        public void UpdateRoom(int RoomId, int daliyRate)
        {
            var existingroom = _roomRepository.GetRoomById(RoomId);
            if (existingroom != null)
            {
                existingroom.DailyRate = daliyRate;

                _roomRepository.UpdateRoom(existingroom);
            }
        }
    }
}
