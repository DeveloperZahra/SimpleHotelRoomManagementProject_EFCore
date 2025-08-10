using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Models
{
    // Represents a booking made by a guest for a specific room within a given period.
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }  //Uniquely identifies each booking record.

        [Required]
        public int GuestId { get; set; }   /// Foreign key linking to the Guest who made this booking.
                                           /// This is a required field.

        [Required]
        public int RoomId { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        // Number of nights stayed
        [NotMapped]  // This tells EF not to map this property to the database, calculated dynamically
        public int Nights
        {
            get
            {
                return (CheckOutDate.Date - CheckInDate.Date).Days;
            }
        }

        // Total cost of booking
        [Column(TypeName = "decimal(18,2)")]  // Define precision for currency in DB
        public decimal TotalCost { get; set; }
    }
}
