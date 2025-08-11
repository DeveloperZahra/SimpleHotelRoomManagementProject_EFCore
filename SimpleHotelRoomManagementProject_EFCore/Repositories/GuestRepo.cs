using SimpleHotelRoomManagementProject_EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    public class GuestRepo : IGuestRepo
    {
        private readonly HotelDbContext _context;
        public GuestRepo(HotelDbContext context)
        {
            _context = context;
        }


        // Add a new guest to the database
        public void AddGuest(Guest guest)
        {
            _context.Guests.Add(guest); // Adds the new guest to the context
            _context.SaveChanges(); // Saves changes to the database
        }

        // Get all guests from the database
        public List<Guest> GetAllGuests()
        {
            return _context.Guests.ToList(); // Fetches all guests from the database
        }


        // Get a guest by their ID
        public Guest GetGuestById(int guestId)
        {
            return _context.Guests.Find(guestId); // Finds a guest by their ID
        }



        // Update an existing guest
        public void UpdateGuest(Guest guest)
        {
            _context.Guests.Update(guest); // Updates the guest in the context
            _context.SaveChanges(); // Saves changes to the database
        }




        //to DeleteGuest method to delete a guest from the database ...
        public void DeleteGuest(int id)
        {
            var guest = GetGuestById(id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
                _context.SaveChanges();
            }
        }

    }
}
