using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Models
{
    // Represents a guest in the hotel management system.
    // Includes validation rules using Data Annotations for EF Core mapping.
    public class Guest
    {
        [Key]
        public int GuestId { get; set; } //Primary key for identifying each guest.

        [Required]
        public string GuestName { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public string GuestEmail { get; set; }

        [Required]
        [RegularExpression(@"^\d{8}$")]
        public string GuestPhoneNumber { get; set; }
    }
}
