using SimpleHotelRoomManagementProject_EFCore.Models;
using SimpleHotelRoomManagementProject_EFCore.Repositories;
using SimpleHotelRoomManagementProject_EFCore.Services;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace SimpleHotelRoomManagementProject_EFCore
{
    internal class Program
    {
        
        // Shared objects (initialized inside Main)
        private static HotelDbContext _context = null!;
        private static IRoomRepo _roomRepo = null!;
        private static IGuestRepo _guestRepo = null!;
        private static IBookingRepo _bookingRepo = null!;
        private static IReviewRepo _reviewRepo = null!;

        private static IRoomServices _roomServices = null!;
        private static IGuestServices _guestServices = null!;
        private static IBookingServices _bookingServices = null!;
        private static IReviewServices _reviewServices = null!;

        static void Main(string[] args)
        { 

        // Create DbContext and repos/services
        _context = new HotelDbContext();

            // Ensure DB is created (shows a message)
            bool isCreated = _context.Database.EnsureCreated();
            if (isCreated)
                Console.WriteLine("Database has been created successfully.");
            else
                Console.WriteLine("Database already exists.");


            // Initialize repositories (use repository when we need direct control)
            _roomRepo = new RoomRepo(_context);
            _guestRepo = new GuestRepo(_context);
            _bookingRepo = new BookingRepo(_context);
            _reviewRepo = new ReviewRepo(_context);

            // Initialize service layer
            _roomServices = new RoomServices(_roomRepo);
            _guestServices = new GuestServices(_guestRepo);
            _bookingServices = new BookingServices(_bookingRepo, _guestRepo, _roomRepo);
            _reviewServices = new ReviewServices(_reviewRepo);

            // Show welcome and enter menu
           
            ShowWelcomeBanner();
            MainMenu(); // start interactive menu loop
        }

        // Display welcome message
        private static void ShowWelcomeBanner()
        {
            Console.Clear();
            Console.WriteLine("############################################################");
            Console.WriteLine("#                                                          #");
            Console.WriteLine("#            WELCOME TO HOTEL ROOM MANAGEMENT               #");
            Console.WriteLine("#                                                          #");
            Console.WriteLine("############################################################");
            Console.WriteLine();
        }

        // Display main menu
        private static void MainMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu - Choose an option:");
                Console.WriteLine("1. Add a new room");
                Console.WriteLine("2. View all rooms");
                Console.WriteLine("3. Reserve a room for a guest");
                Console.WriteLine("4. View all Booking with total cost");
                Console.WriteLine("5. Search reservation by guest name");
                Console.WriteLine("6. Find the highest-paying guest");
                Console.WriteLine("7. Cancel a Booking by room number");
                Console.WriteLine("8. Exit the system");
                Console.Write("Enter your choice (1-8): ");

                string? choice = Console.ReadLine();

                Console.WriteLine(); // spacer

                switch (choice)
                {
                    case "1":
                        Menu_AddNewRoom();
                        break;
                    case "2":
                        Menu_ViewAllRooms();
                        break;
                    case "3":
                        Menu_ReserveRoomForGuest();
                        break;
                    case "4":
                        Menu_ViewAllBookingWithTotal();
                        break;
                    case "5":
                        Menu_SearchBookingByGuestName();
                        break;
                    case "6":
                        Menu_FindHighestPayingGuest();
                        break;
                    case "7":
                        Menu_CancelBookingByRoomNumber();
                        break;
                    case "8":
                        Console.WriteLine("Exiting the system. Goodbye!");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 8.");
                        break;
                }
            }
        }
        //1) Add a new room (Room Number, Daily Rate)
        // Uses repository.AddRoom so we can set RoomNumber explicitly (service method in project
        /// only accepts dailyRate & availability).
        private static void Menu_AddNewRoom()
        {
            Console.WriteLine("Add a new room:");

            Console.Write(" - Room Number: ");
            int roomNumber = InputValidator.GetPositiveInt();

            Console.Write(" - Daily Rate: ");
            decimal dailyRate = InputValidator.GetPositiveDecimal();

            // Create room using model fields available in your project
            var room = new Room
            {
                RoomNumber = roomNumber,
                DailyRate = dailyRate,
                IsReserved = false // new rooms are available by default
            };

            // Use repository directly so RoomNumber is saved exactly as entered
            _roomRepo.AddRoom(room);

            Console.WriteLine($"Room {roomNumber} added successfully with daily rate {dailyRate:C}.");
        }

        //2) View all rooms (Available and Reserved)
        // Uses room repository to get full list and prints IsReserved status.
        private static void Menu_ViewAllRooms()
        {
            Console.WriteLine("All rooms (Available / Reserved):");

            List<Room> rooms = _roomRepo.GetAllRooms();

            if (rooms == null || rooms.Count == 0)
            {
                Console.WriteLine("No rooms found.");
                return;
            }

            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("ID | RoomNumber | DailyRate | Reserved?");
            Console.WriteLine("------------------------------------------------------");
            foreach (var r in rooms)
            {
                string reservedText = r.IsReserved ? "Reserved" : "Available";
                Console.WriteLine($"{r.RoomId} | {r.RoomNumber} | {r.DailyRate:C} | {reservedText}");
            }
            Console.WriteLine("------------------------------------------------------");
        }


        /// <summary>
        /// 3) Reserve a room for a guest (Guest Name, Room Number, Nights)
        /// Steps:
        ///  - find guest by name (case-insensitive) or create new guest if not found
        ///  - find room by RoomNumber and ensure it's not reserved
        ///  - call BookingServices.AddNewBooking(...) which enforces booking logic in services layer
        ///    (the project BookingServices signature: AddNewBooking(int nights, DateTime checkInDate, int guestId, int roomId))
        ///  - if booking is successful, mark the room reserved via room services
        /// </summary>

        private static void Menu_ReserveRoomForGuest()
        {
            Console.WriteLine("Reserve a room for a guest:");

            Console.Write(" - Guest Name: ");
            string guestName = InputValidator.GetNonEmptyString();

            Console.Write(" - Room Number: ");
            int roomNumber = InputValidator.GetPositiveInt();

            Console.Write(" - Nights: ");
            int nights = InputValidator.GetPositiveInt();

            // Find guest (case-insensitive) in repository
            var guests = _guestRepo.GetAllGuests();
            var matchedGuest = guests
                .FirstOrDefault(g => string.Equals(g.GuestName?.Trim(), guestName.Trim(), StringComparison.OrdinalIgnoreCase));

            if (matchedGuest == null)
            {
                Console.WriteLine("Guest not found. We will create a new guest record.");

                // Ask for minimal info - email and phone (project's Guest model expects these)
                Console.Write(" - Guest Email: ");
                string email = InputValidator.GetNonEmptyString();

                Console.Write(" - Guest Phone (8 digits expected by your model): ");
                string phone = InputValidator.GetNonEmptyString();

                var newGuest = new Guest
                {
                    // GuestId typically generated by DB; repo.AddGuest will save and set it
                    GuestName = guestName,
                    GuestEmail = email,
                    GuestPhoneNumber = phone
                };

                _guestRepo.AddGuest(newGuest);
                matchedGuest = newGuest; // now use the newly created guest
                Console.WriteLine($"Created guest '{matchedGuest.GuestName}' with ID {matchedGuest.GuestId}.");
            }


            // Find room by RoomNumber
            var allRooms = _roomRepo.GetAllRooms();
            var room = allRooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

            if (room == null)
            {
                Console.WriteLine($"Room with number {roomNumber} does not exist. Reservation aborted.");
                return;
            }

            if (room.IsReserved)
            {
                Console.WriteLine($"Room {roomNumber} is already reserved. Choose another room or cancel existing reservation first.");
                return;
            }

            // Determine check-in date: using NOW as default (menu did not ask for date)
            DateTime checkInDate = DateTime.Now;

            // Use booking service to create booking (it will calculate totals, set booking fields in project logic)
            var result = _bookingServices.AddNewBooking(nights, checkInDate, matchedGuest.GuestId, room.RoomId);

            if (result.Ok)
            {

                // Mark room as reserved through room service (the service exposes an UpdateRoom(RoomId, bool isReserved) method)
                _roomServices.UpdateRoom(room.RoomId, true);

                Console.WriteLine($"Reservation successful. Booking ID: {result.BookingId}. Message: {result.Message}");
            }
            else
            {
                Console.WriteLine($"Reservation failed: {result.Message}");
            }
        }

        //4) View all reservations with total cost
        // Prints each booking with Guest, RoomNumber, Nights and TotalCost.
        /// If TotalCost was not set by service, compute using Room.DailyRate * Nights.
        private static void Menu_ViewAllBookingWithTotal()
        {
            Console.WriteLine("All reservations:");

            var bookings = _bookingRepo.GetAllBooking();

            if (bookings == null || bookings.Count == 0)
            {
                Console.WriteLine("No reservations found.");
                return;
            }

            Console.WriteLine("--------------------------------------------------------------------------");
            Console.WriteLine("BookingId | GuestName | RoomNumber | Nights | TotalCost | Status");
            Console.WriteLine("--------------------------------------------------------------------------");

            foreach (var b in bookings)
            {
                // Try to ensure we have up-to-date navigation properties:
                var full = _bookingRepo.GetBookingById(b.BookingId); // get single with any navigation loaded by repo
                string guestName = full.Guest?.GuestName ?? "(unknown)";
                int roomNumber = full.Room?.RoomNumber ?? b.RoomId; // fallback
                int nights = full.Nights;
                decimal total = full.TotalCost;

                if (total == 0 && full.Room != null)
                {
                    total = full.Room.DailyRate * full.Nights;
                }

                Console.WriteLine($"{full.BookingId} | {guestName} | {roomNumber} | {nights} | {total:C} | {full.Status}");
            }

            Console.WriteLine("--------------------------------------------------------------------------");
        }

        // 5) Search reservation by guest name (case-insensitive)
       // Filters bookings and prints matched records
        private static void Menu_SearchBookingByGuestName()
        {
            Console.Write("Enter guest name to search (case-insensitive): ");
            string name = InputValidator.GetNonEmptyString();

            var allBookings = _bookingRepo.GetAllBooking();

            var matches = allBookings.Where(b =>
            {
                // Ensure Guest is present; compare case-insensitive
                var guestName = b.Guest?.GuestName ?? string.Empty;
                return guestName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0;
            }).ToList();

            if (matches.Count == 0)
            {
                Console.WriteLine("No reservations found for that guest.");
                return;
            }

            Console.WriteLine("Matched reservations:");
            foreach (var b in matches)
            {
                var full = _bookingRepo.GetBookingById(b.BookingId);
                string guestName = full.Guest?.GuestName ?? "(unknown)";
                int roomNumber = full.Room?.RoomNumber ?? full.roomId;
                decimal total = full.TotalCost;
                if (total == 0 && full.Room != null) total = full.Room.DailyRate * full.Nights;
                Console.WriteLine($"Booking {full.BookingId} | Guest: {guestName} | Room: {roomNumber} | Nights: {full.Nights} | Total: {total:C}");
            }
        }

        // 6) Find the highest-paying guest
        // Sums total cost per guest and finds the maximum
        private static void Menu_FindHighestPayingGuest()
        {
            var bookings = _bookingRepo.GetAllBooking();
            if (bookings == null || bookings.Count == 0)
            {
                Console.WriteLine("No bookings available to analyze.");
                return;
            }

            // Compute totals per guest
            var totals = new Dictionary<int, decimal>(); // guestId -> total
            var guestNames = new Dictionary<int, string>();

            foreach (var b in bookings)
            {
                var full = _bookingRepo.GetBookingById(b.BookingId);
                int guestId = full.Guest?.GuestId ?? 0;
                string guestName = full.Guest?.GuestName ?? "(unknown)";

                decimal total = full.TotalCost;
                if (total == 0 && full.Room != null) total = full.Room.DailyRate * full.Nights;

                if (!totals.ContainsKey(guestId))
                {
                    totals[guestId] = total;
                    guestNames[guestId] = guestName;
                }
                else
                {
                    totals[guestId] += total;
                }
            }

            // Find highest
            var highest = totals.OrderByDescending(kv => kv.Value).FirstOrDefault();
            if (highest.Key == 0 && highest.Value == 0)
            {
                Console.WriteLine("Could not determine highest-paying guest.");
                return;
            }

            Console.WriteLine($"Highest-paying guest is: {guestNames[highest.Key]} (GuestId: {highest.Key}) with total payments: {highest.Value:C}");
        }


        // 7) Cancel a reservation by room number
        /// Finds the (first) active booking for the given room number and cancels it.
        /// Also sets the room's IsReserved to false.

        private static void Menu_CancelBookingByRoomNumber()
        {
            Console.Write("Enter room number to cancel the reservation for: ");
            int roomNumber = InputValidator.GetPositiveInt();

            // Find bookings for that room number
            var bookings = _bookingRepo.GetAllBooking();
            // Get bookings whose related room has the required RoomNumber
            var candidate = bookings.FirstOrDefault(b => (b.Room != null && b.Room.RoomNumber == roomNumber)
                                                         || (b.roomId != 0 && // fallback if navigation not loaded
                                                             _roomRepo.GetRoomById(b.roomId)?.RoomNumber == roomNumber));

            if (candidate == null)
            {
                Console.WriteLine($"No reservation found for room number {roomNumber}.");
                return;
            }





        }
    }
}
