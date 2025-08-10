using SimpleHotelRoomManagementProject_EFCore.Models;
using SimpleHotelRoomManagementProject_EFCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleHotelRoomManagementProject_EFCore.Services
{
    public  class GuestServices
    {
        // Constructor Injection 
        private readonly IGuestRepo _guestRepository;
        public GuestServices(IGuestRepo guestRepository) 
        {
            _guestRepository = guestRepository ?? throw new ArgumentNullException(nameof(guestRepository));
        }

        // Add methods for guest services here, e.g., GetGuestById, AddGuest, UpdateGuest, DeleteGuest, etc.
        // Add new Guest
        public void AddNewGuest(int guestId, string guestname, string guestemail, string GuestPhoneNo)
        {
            var guest = new Guest
            {
                GuestId = guestId,
                GuestName = guestname,
                GuestEmail = guestemail,
                GuestPhoneNumber = GuestPhoneNo

            };
            _guestRepository.AddGuest(guest);
        }


        // Get All Guest
        public void GetAllGuest()
        {
            _guestRepository.GetAllGuests();
        }


        // get guest by id 
        public void GetGuestById(int guestId)
        {
            _guestRepository.GetGuestById(guestId);
        }







    }


}
