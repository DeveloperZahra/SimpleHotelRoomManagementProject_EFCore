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
        public int RoomId { get; set; } // Foreign key linking to the Room that is booked.

        [Required]
        public DateTime CheckInDate { get; set; } // The scheduled date when the guest will check in

        [Required]
        public DateTime CheckOutDate { get; set; } // The scheduled date when the guest will check out

        // Number of nights stayed
        [NotMapped]  // This tells EF not to map this property to the database, calculated dynamically

        // Calculates the total number of nights for the booking.
        /// This property is not stored in the database as it is dynamically calculated
        /// based on the difference between CheckOutDate and CheckInDate.
        public int Nights
        {
            get
            {
                return (CheckOutDate.Date - CheckInDate.Date).Days;
            }
        }

        // Total cost of booking
        [Column(TypeName = "decimal(18,2)")]  // Define precision for currency in DB
        public decimal TotalCost { get; set; } // This should be set externally based on room rates and number of nights
    }
}
