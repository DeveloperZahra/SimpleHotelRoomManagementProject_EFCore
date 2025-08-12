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





















    }
}
}
