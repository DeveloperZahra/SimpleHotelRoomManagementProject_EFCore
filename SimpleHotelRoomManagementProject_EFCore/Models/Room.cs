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
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }

        [Required]
        public decimal DailyRate { get; set; }

        public bool IsReserved { get; set; }

    }
}
