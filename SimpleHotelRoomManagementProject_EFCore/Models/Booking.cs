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

      // Ensures the property is required (cannot be null or missing) 
      // and must have a value between 1 and the maximum integer value.
        [Required, Range(1, int.MaxValue)]
        public int Nights { get; set; }  // Number of nights stayed

        // Total cost of booking
        [Column(TypeName = "decimal(18,2)")]  // Define precision for currency in DB
        public decimal TotalCost { get; set; } // This should be set externally based on room rates and number of nights


        public string Status { get; set; } // The current status of the booking (e.g., "Confirmed", "Cancelled", "Pending")




        // One-to-One navigation property
        public Review Review { get; set; } = null;

        // Forign key relationships
        [ForeignKey("Guest")]
        public int guestId { get; set; }
        public Guest Guest { get; set; } // navigation to guest 

        [ForeignKey("Room")]
        public int roomId { get; set; }
        public Room Room { get; set; } // navigation to room
    }




}
