using SimpleHotelRoomManagementProject_EFCore.Models;
using SimpleHotelRoomManagementProject_EFCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Services
{
    //Service class that implements booking-related business logic
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepo _bookingRepository;
        private readonly IGuestRepo _guestRepository;
        private readonly IRoomRepo _roomRepository;

        // Constructor injecting all necessary repositories
        public BookingServices(IBookingRepo bookingRepository, IGuestRepo guestRepository, IRoomRepo roomRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _guestRepository = guestRepository ?? throw new ArgumentNullException(nameof(guestRepository));
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }

        public (bool Ok, string Message, int? BookingId) AddNewBooking(int nights, DateTime checkInDate, int guestId, int roomId)
        {
            // 1) Validate number of nights
            if (nights <= 0)
                return (false, "Number of nights must be greater than 0.", null);

            // 2) Normalize check-in date and validate it is not in the past
            checkInDate = checkInDate.Date;
            if (checkInDate < DateTime.Today)
                return (false, "Check-in date cannot be in the past.", null);

            var checkOutDate = checkInDate.AddDays(nights);

            // 3) Check if room exists
            var room = _roomRepository.GetRoomById(roomId);
            if (room == null)
                return (false, $"No room found with ID {roomId}.", null);

            // 4) Check if guest exists
            var guest = _guestRepository.GetGuestById(guestId);
            if (guest == null)
                return (false, $"No guest found with ID {guestId}.", null);

            // 5) Check for overlapping bookings
            bool overlaps = _bookingRepository.ExistsOverlap(roomId, checkInDate, checkOutDate);
            if (overlaps)
                return (false, "Room is not available for the selected dates.", null);

            // 6) Calculate total cost
            decimal totalCost = room.PricePerNight * nights;

            // 7) Create booking object
            var booking = new Booking
            {
                Nights = nights,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                GuestId = guestId,
                RoomId = roomId,
                TotalCost = totalCost,
                Status = BookingStatus.Confirmed.ToString() // use enum or constant here
            };

            try
            {
                // 8) Save booking
                _bookingRepository.AddBooking(booking);
            }
            catch (Exception ex)
            {
                // Log error (if you have a logger) and return failure message
                return (false, $"Error saving booking: {ex.Message}", null);
            }

            // 9) Return success with the new BookingId
            return (true, "Booking created successfully.", booking.BookingId);
        }
    }

    // Example enum for booking status (define outside this class)
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }

}
