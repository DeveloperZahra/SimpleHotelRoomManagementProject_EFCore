using SimpleHotelRoomManagementProject_EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    public  class GuestRepo
    {
        private readonly HotelDbContext _context;
        public GuestRepo(HotelDbContext context)
        {
            _context = context;
        }


        // Add a new guest to the database
        public void AddGuest(Guest guest)
        {
            _context.Guest.Add(guest); // Adds the new guest to the context
            _context.SaveChanges(); // Saves changes to the database
        }




    }
}
