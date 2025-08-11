using SimpleHotelRoomManagementProject_EFCore.Models;
using SimpleHotelRoomManagementProject_EFCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Services
{
    // Service class that handles business logic related to bookings 
    public class BookingServices : IBookingServices
    {
        
        private readonly IBookingRepo _bookingRepository;
        private readonly IGuestRepo _guestRepository;
        private readonly IRoomRepo _roomRepository;

        // Constructor: inject all required repositories in one go
        public BookingServices(IBookingRepo bookingRepository)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
        }


        private readonly IGuestRepo _GuestRepo;
        public BookingServices(IGuestRepo guestRepository)
        {
            _GuestRepo = guestRepository ?? throw new ArgumentNullException(nameof(guestRepository));
        }


        private readonly IRoomRepo _roomRepository;
        public BookingServices(IRoomRepo roomRepository)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }

        public (bool Ok, string Message, int? BookingId) AddNewBooking(int BookingId, int nights, DateTime checkInDate, int guestId, int roomId, decimal TotalCost, string Status)
        {
            // 1) validation for number of night
            if (nights <= 0) return (false, "Number of nights must be greater than 0.", null);

            checkInDate = checkInDate.Date; // normalize to date (00:00)
            var checkOutDate = checkInDate.AddDays(nights);

            // 2) check if room or guest exist
            var room = _roomRepository.GetRoomById(roomId);
            if (room == null) return (false, "There is no room with this id number.", null);

            // 3) Check if the guest exists
            var guest = _GuestRepo.GetGuestById(guestId);
            if (guest == null) return (false, "There is no guest with this id number.", null);

            // 4) avoid booking same room in same date this state named overlaps 
            // Overlaps rule: (startA < endB) && (endA > startB)
            bool overlaps = _bookingRepository.ExistsOverlap(
                roomId, checkInDate, checkOutDate);

            if (overlaps)
                return (false, "Room is not available in the selected dates.", null);



            // 5) Calculate total cost
            decimal totalCost = room.PricePerNight * nights;

            // 6) Create booking object
            var booking = new Booking
            {
                Nights = nights,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                GuestId = guestId,
                RoomId = roomId,
                TotalCost = totalCost,
                Status = "Confirmed"
            };

            // 7) Save booking
            _bookingRepository.AddBooking(booking);

            // Return success with new BookingId
            return (true, "Booking created successfully.", booking.BookingId);


        }
    }
