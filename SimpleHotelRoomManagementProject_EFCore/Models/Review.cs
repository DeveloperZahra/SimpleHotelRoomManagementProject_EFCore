using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleHotelRoomManagementProject_EFCore.Models
{
    // Represents a guest review for a specific booking in the hotel management system.
    // Includes validation rules using Data Annotations for EF Core.
    public class Review
    {
        [Key] // Primary key
        public int ReviewId { get; set; } //Primary key for uniquely identifying each review

        [Required] // Must be linked to a booking
        [ForeignKey("Booking")]
        public int BookingId { get; set; } //Foreign key linking this review to a specific booking.

        [Required] // Must be linked to a guest
        [ForeignKey("Guest")]
        public int GuestId { get; set; } //Foreign key linking this review to the guest who wrote it

        [Required] // Review Comment  is mandatory
        [MaxLength(500)] // Limit text length to 500 characters
        public string Comment { get; set; } //Required textual feedback from the guest, limited to 500 characters

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")] //Optional numeric rating from 1 to 5 stars.
        public int? Rating { get; set; } // Optional star rating

        [Required] // Review date is mandatory
        public DateTime ReviewDate { get; set; } = DateTime.Now; // Date and time when the review was created(defaults to current date/time).

        public Booking booking { get; set; } = default!; // Navigation property to Room

    }
}
