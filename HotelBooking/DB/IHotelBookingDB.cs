using HotelBooking.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DB
{
    public interface IHotelBookingDb
    {
        //TODO: GetBookingById

        IEnumerable<RoomBookingInfo> GetBookedDaysByRoom();
        Task MakeNewBookingAsync(Booking booking);
        Task ResetHotelSizeAsync(int size);
    }
}
