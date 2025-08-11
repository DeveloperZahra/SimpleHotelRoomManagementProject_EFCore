using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Models
{
    // Represents a hotel room entity in the system.
    // Uses Data Annotations for EF Core mapping.
    public class Room
    {
        [Key]// Marks this property as the Primary Key in the database
        public int RoomId { get; set; } // RoomId: Primary key for database table
        public int RoomNumber { get; set; } // RoomNumber: Unique room identifier visible to guests/admin

        [Required]
        public decimal DailyRate { get; set; } // DailyRate: Required daily rental cost of the room.

        public bool IsReserved { get; set; } // IsReserved: Tracks whether the room is currently booked.
        public int PricePerNight { get; internal set; } // The price charged for renting the room per night.
                                                        // The setter is internal to restrict modification outside the assembly

        public ICollection<Booking> RoomBooking { get; set; } // Navigation property for booking class
    }
}
