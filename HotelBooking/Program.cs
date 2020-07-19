using HotelBooking.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//test test
namespace HotelBooking
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IBookingManager bookingManager = new BookingManager();
            int hotelSize = 0;
            do
            {
                Console.WriteLine("Please select the hotel size (must be an integer).");
                Console.WriteLine("Size: ");
                var hotelSizeStr = Console.ReadLine();
                Int32.TryParse(hotelSizeStr, out hotelSize);
            } while (hotelSize <= 0);

            await bookingManager.ResetHotelSizeAsync(hotelSize);

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                Console.WriteLine("Make a booking.");
                Console.WriteLine("Start date:");
                var startStr = Console.ReadLine();
                if (!Int32.TryParse(startStr, out int start))
                {
                    Console.WriteLine("Start date must be an integer. Please try again.");
                    continue;
                }

                Console.WriteLine("End date:");
                var endStr = Console.ReadLine();
                if (!Int32.TryParse(endStr, out int end))
                {
                    Console.WriteLine("End date must be an integer. Please try again.");
                    continue;
                }

                var result = await bookingManager.TryBookARoomAsync(start, end);
                if (result.Item1) //booking successful
                {
                    Console.WriteLine($"Successfully booked the Room {result.Item2}.");
                }
                else
                {
                    Console.WriteLine($"The hotel is overbooked. Please try some other timeframe.");
                }


                Console.WriteLine("**********************************");
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
