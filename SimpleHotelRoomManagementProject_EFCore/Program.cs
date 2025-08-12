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

























        }
    }
}
