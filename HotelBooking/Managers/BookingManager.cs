using HotelBooking.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//test
namespace HotelBooking.Managers
{
    public class BookingManager : IBookingManager
    {
        private readonly IHotelBookingDb _bookingDb;
        public BookingManager()
        {
            _bookingDb = new HotelBookingDb();
        }

        public async Task<(bool, int)> TryBookARoomAsync(int startDate, int endDate)
        {
            var roomBookingInfos = _bookingDb.GetBookedDaysByRoom();
            if (!roomBookingInfos.Any()) //if there are no rooms, booking cannot be made
            {
                return (false, 0);
            }

            var firstAvailableRoomId = roomBookingInfos.FirstOrDefault(rbi => !rbi.BookedDays.Any(bd => bd >= startDate && bd <= endDate))?.Id;
            if (firstAvailableRoomId == null) //if there is no room available, booking cannot be made
            {
                return (false, 0);
            }


            //make the booking
            var newBooking = new Booking
            {
                RoomId = firstAvailableRoomId.Value,
                StartDate = startDate,
                EndDate = endDate
            };
            await _bookingDb.MakeNewBookingAsync(newBooking);

            return (true, firstAvailableRoomId.Value);
        }

        public async Task ResetHotelSizeAsync(int size)
        {
            await _bookingDb.ResetHotelSizeAsync(size);
        }
    }

    public interface IBookingManager
    {
        Task<(bool, int)> TryBookARoomAsync(int startDate, int endDate);
        Task ResetHotelSizeAsync(int size);
    }
}
