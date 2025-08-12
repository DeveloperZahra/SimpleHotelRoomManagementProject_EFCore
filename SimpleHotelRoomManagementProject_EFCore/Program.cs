using SimpleHotelRoomManagementProject_EFCore.Models;
using SimpleHotelRoomManagementProject_EFCore.Repositories;
using SimpleHotelRoomManagementProject_EFCore.Services;

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
                        Menu_ViewAllReservationsWithTotal();
                        break;
                    case "5":
                        Menu_SearchReservationByGuestName();
                        break;
                    case "6":
                        Menu_FindHighestPayingGuest();
                        break;
                    case "7":
                        Menu_CancelReservationByRoomNumber();
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
        //implement menu_addnewroom method 
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

        //implement menu_viewallrooms method 
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









        }
    }
}
