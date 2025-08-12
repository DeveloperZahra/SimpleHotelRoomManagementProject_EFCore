using SimpleHotelRoomManagementProject_EFCore.Repositories;
using SimpleHotelRoomManagementProject_EFCore.Services;

namespace SimpleHotelRoomManagementProject_EFCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Welcome Message
            Console.WriteLine("====================================");
            Console.WriteLine(" Welcome to the Simple Hotel System ");
            Console.WriteLine("====================================\n");

            // Create DbContext
            using var context = new HotelDbContext();

            // Initialize repositories
            var roomRepo = new RoomRepo(context);
            var bookingRepo = new BookingRepo(context);
            var guestRepo = new GuestRepo(context);
            var reviewRepo = new ReviewRepo(context);

            // Initialize services
            var roomService = new RoomServices(roomRepo);
            var bookingService = new BookingServices(bookingRepo, guestRepo, roomRepo);

            bool exit = false; // Flag to exit the program

            // Main Menu Loop
            // Keeps running until the user chooses to exit
            while (!exit)
            {

                // Display menu options
                Console.WriteLine("\n===== Main Menu =====");
                Console.WriteLine("1. Add Room");
                Console.WriteLine("2. View All Rooms");
                Console.WriteLine("3. Add Booking");
                Console.WriteLine("4. View All Bookings");
                Console.WriteLine("5. Search Booking by Guest Name");
                Console.WriteLine("6. Cancel Booking");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                // Read user's choice
                string choice = Console.ReadLine();

                // Process user's choice
                switch (choice)
                {


















                }
    }
}
