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





















            }
        }
}
