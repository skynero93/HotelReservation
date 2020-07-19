using HotelBooking.SharedTools;
using HotelBooking.DB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DB
{
    public class HotelBookingDb : IHotelBookingDb
    {
        //TODO: GetBookingById

        public IEnumerable<RoomBookingInfo> GetBookedDaysByRoom()
        {
            using (HotelBookingEntities db = new HotelBookingEntities())
            {
                var rooms = db.Rooms.ToArray();
                var roomIds = rooms.Select(r => r.Id);
                var bookings = db.Bookings.Where(b => roomIds.Contains(b.RoomId)).ToArray();

                //get booking days for all rooms
                var roomBookingInfos = rooms.Select(r =>
                {
                    //get all booking durations as (start, end) tuples for the room
                    var startEndTuples = bookings
                            .Where(b => b.RoomId == r.Id)
                            .Select(b => (b.StartDate, b.EndDate));

                    //get list of all booked days for the room
                    var bookedDays = Helpers.GetNumbersBetween(startEndTuples);

                    //build the booking info
                    var roomBookingInfo = new RoomBookingInfo
                    {
                        Id = r.Id,
                        Name = r.Name,
                        BookedDays = bookedDays
                    };

                    return roomBookingInfo;
                });

                return roomBookingInfos;
            }
        }

        public async Task MakeNewBookingAsync(Booking booking)
        {
            using (HotelBookingEntities db = new HotelBookingEntities())
            {
                db.Bookings.Add(booking);

                await db.SaveChangesAsync();

                //return bookingId;
            };
        }

        public async Task ResetHotelSizeAsync(int size)
        {
            using (HotelBookingEntities db = new HotelBookingEntities())
            {
                //truncate the table
                string tableName = "Rooms"; //for now, use the "magic string", but the table name should be retrieved in a dynamic way (there is no built-in way for doing this, so some helper method should be implemented)...
                db.Database.ExecuteSqlCommand($"TRUNCATE TABLE {tableName}");

                //add rooms according to the size
                Room[] rooms = new Room[size];
                for (int i = 1; i <= size; i++)
                {
                    rooms[i - 1] = new Room
                    {
                        Id = i,
                        Name = $"Room {i}"
                    };
                }
                db.Rooms.AddRange(rooms);

                await db.SaveChangesAsync();
            }
        }
    }
}
