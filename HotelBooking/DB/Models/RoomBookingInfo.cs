using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.DB.Models
{
    public class RoomBookingInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> BookedDays { get; set; }
    }
}
