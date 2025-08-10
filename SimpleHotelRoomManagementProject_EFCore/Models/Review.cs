using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Models
{
    // Represents a guest review for a specific booking in the hotel management system.
    // Includes validation rules using Data Annotations for EF Core.
    public class Review
    {
        [Key] // Primary key
        public int ReviewId { get; set; }

        [Required] // Must be linked to a booking
        [ForeignKey("Booking")]
        public int BookingId { get; set; }

        [Required] // Must be linked to a guest
        [ForeignKey("Guest")]
        public int GuestId { get; set; }

        [Required] // Review Comment  is mandatory
        [MaxLength(500)] // Limit text length to 500 characters
        public string Comment { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int? Rating { get; set; } // Optional star rating

        [Required] // Review date is mandatory
        public DateTime ReviewDate { get; set; } = DateTime.Now;

    }
}
