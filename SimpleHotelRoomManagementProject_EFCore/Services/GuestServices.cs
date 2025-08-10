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
    }
}
