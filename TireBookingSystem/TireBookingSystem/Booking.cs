using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireBookingSystem
{
    public class Booking
    {
        public static void SearchFreeTimes() 
        {
            Console.WriteLine("Här kommer lediga tider att visas senare i koden");
            Console.WriteLine("Tryck på en valfri tangent för att gå vidare");
            Console.ReadKey();
        }

        public static void AddBooking()
        {
            Console.WriteLine("Här kommer man kunna boka en tid senare");
            Console.WriteLine("Tryck på en valfri tangent för att gå vidare");
            Console.ReadKey();
        }

        public static void CancelBooking() 
        {
            Console.WriteLine("Här kommer tider att avbokas senare");
            Console.WriteLine("Tryck på valfri tangent för att gå vidare");
            Console.ReadKey();
        }

        public static void ListBookings()
        {
            Console.WriteLine("Här kommer alla bokningar att visas senare i koden");
            Console.WriteLine("Tryck på valfri tangent för att gå tillbaka");
            Console.ReadKey();
        }
    }
}
