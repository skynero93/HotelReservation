using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.DB;

namespace HotelBooking.Tests
{
    [TestClass]
    public class MainTests
    {
        private readonly IHotelBookingDb _bookingDb;

        public MainTests()
        {
            _bookingDb = new HotelBookingDb();
        }

        [TestMethod]
        public async Task TestCase1b()
        {
            var newBooking = new Booking
            {
                RoomId = 1,
                StartDate = -4,
                EndDate = 2
            };

            //var bookingId = await _bookingDb.MakeNewBookingAsync(newBooking);
            await _bookingDb.MakeNewBookingAsync(newBooking);

            //TODO:
            // - get booking by id
            Booking booking = null; //GetBookingById(bookingId)

            //if not found, OK
            Assert.IsNull(booking, "Booking shouldn't be made!");
        }
    }
}
