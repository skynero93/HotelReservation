using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.SharedTools
{
    public static class Helpers
    {
        public static List<int> GetNumbersBetween(IEnumerable<(int, int)> startEndTuples)
        {
            var numbers = new List<int>();
            foreach ((int, int) startEndTuple in startEndTuples)
            {
                for (int i = startEndTuple.Item1; i <= startEndTuple.Item2; i++)
                {
                    numbers.Add(i);
                }
            }

            return numbers;
        }

        public static List<int> GetNumbersBetween(int start, int end)
        {
            List<int> numbers = new List<int>();
            for (int i = start; i <= end; i++)
            {
                numbers.Add(i);
            }

            return numbers;
        }

        public static List<DateTime> GetDatesBetween(DateTime start, DateTime end)
        {
            List<DateTime> dates = new List<DateTime>();
            for (DateTime date = start; date <= end; date.AddDays(1))
            {
                dates.Add(date);
            }

            return dates;
        }
    }
}
