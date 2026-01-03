using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireBookingSystem
{

    public struct CustomerName 
    {
        public string FirstName;
        public string LastName;
    }

    public struct VehicleInfo 
    {
        public string RegistrationNumber;
    }

    public enum ServiceType 
    {
        TireChange = 1,
        Balancing,
        Tirestorage,
        Puncturerepair
    }

    public class Booking
    {

        public CustomerName Name { get; set; }
        public VehicleInfo Vehicle { get; set; }
        public DateTime BookingDate { get; set; }
        public ServiceType Service { get; set; }

        public static List<Booking> BookingList = new List<Booking>();

        public Booking(CustomerName name, VehicleInfo vehicle, DateTime date, ServiceType service)
        {
            Name = name;
            Vehicle = vehicle;
            BookingDate = date;
            Service = service;
        }

        public static void SearchFreeTimes() 
        {
            Console.WriteLine("Här kommer lediga tider att visas senare i koden");
            Console.WriteLine("Tryck på en valfri tangent för att gå vidare");
            Console.ReadKey();
        }

        public static void AddBooking()
        {
            Console.Clear();
            Console.WriteLine("Ny tidsbokning");
            Console.WriteLine();

            Console.Write("Ange förnamn: ");
            string FirstName = Console.ReadLine();
            Console.Write("Ange efternamn: ");
            string LastName = Console.ReadLine();
            Console.Write("Ange registreringsnummer: ");
            string RegistrationNumber = Console.ReadLine();

            ServiceType selectedService = ServiceType.TireChange;
            bool isServiceValid = false;

            while (!isServiceValid)
            {
                Console.WriteLine();
                Console.WriteLine("Välj tjänst:");
                Console.WriteLine("1: Däckbyte");
                Console.WriteLine("2: Däckbalansering");
                Console.WriteLine("3: Däckförvaring");
                Console.WriteLine("4: Punkteringslagning");
                Console.WriteLine();
                Console.Write("Välj en tjänst (1-4): ");
                string serviceChoice = Console.ReadLine();

                switch (serviceChoice)
                {
                    case "1":
                        selectedService = ServiceType.TireChange;
                        isServiceValid = true;
                        break;
                    case "2":
                        selectedService = ServiceType.Balancing;
                        isServiceValid = true;
                        break;
                    case "3":
                        selectedService = ServiceType.Tirestorage;
                        isServiceValid = true;
                        break;
                    case "4":
                        selectedService = ServiceType.Puncturerepair;
                        isServiceValid = true;
                        break;
                    default:
                        Console.WriteLine("\nOgiltigt val, vänligen ange en siffra mellan 1-4.");
                        break;
                }
            }

            int year, month, day, hour, minute;
            DateTime date = DateTime.Now;
            bool isDateValid = false;

            while (!isDateValid) 
            {
                Console.WriteLine();
                Console.WriteLine("Ange datum och tid för din bokning verkstaden är öppen mellan 08:00-18:00");
                Console.WriteLine();

                Console.WriteLine("Ange ett datum (ÅÅÅÅ-MM-DD): ");
                string dateInput = Console.ReadLine();
                string[] dateParts = dateInput.Split('-');

                if (dateParts.Length == 3 &&
                     int.TryParse(dateParts[0], out year) &&
                     int.TryParse(dateParts[1], out month) &&
                     int.TryParse(dateParts[2], out day))

                {   Console.Write("Ange en tid mellan (08:00-17:30): ");
                    string timeInput = Console.ReadLine();

                    string[] timeParts = timeInput.Split(':');

                    if (timeParts.Length == 2 &&
                        int.TryParse(timeParts[0], out hour) &&
                        int.TryParse(timeParts[1], out minute))
                    {
                        if (hour >= 8 && hour <= 17 && minute >= 0 && minute < 60)
                        {
                            try
                            {
                                date = new DateTime(year, month, day, hour, minute, 0);

                                if (date < DateTime.Now)
                                {
                                    Console.WriteLine("\nFel, du kan inte boka en tid som redan har passerat");
                                    continue;
                                }

                                bool isOccupied = BookingList.Any(b => b.BookingDate == date);

                                if (isOccupied)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("\nTyvärr är den här tiden redan bokad");
                                    continue;
                                }

                                isDateValid = true;
                                break;

                            }
                            catch
                            {
                                Console.WriteLine();
                                Console.WriteLine("\nOgiltigt datum kontrollera att året, månaden och dagen stämmer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\nTiden måste vara mellan 08:00 och 17:30. Försök igen.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nOgiltigt tidsformat, använd (HH:mm)");
                    }
                }
                else
                {
                    Console.WriteLine("\nOgiltigt datumformat, använd (ÅÅÅÅ-MM-DD)");
                }
            }

            CustomerName customerName = new CustomerName { FirstName = FirstName, LastName = LastName };
            VehicleInfo vehicleInfo = new VehicleInfo { RegistrationNumber = RegistrationNumber };

            Booking newBooking = new Booking(customerName, vehicleInfo, date, selectedService);
            BookingList.Add(newBooking);

            Console.WriteLine();
            Console.WriteLine("Bokningen är nu registrerad!");
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
            Console.Clear();
            Console.WriteLine("Alla bokningar");
            Console.WriteLine();

            if (BookingList.Count == 0) 
            {
                Console.WriteLine("Det finns inga bokningar registrerade");
            }
            else
            {
                foreach (var booking in BookingList)
                {
                    string serviceSwedish = booking.Service switch
                    {
                        ServiceType.TireChange => "Däckbyte",
                        ServiceType.Balancing => "Balansering",
                        ServiceType.Tirestorage => "Däckförvaring",
                        ServiceType.Puncturerepair => "Punkteringslagning",
                        _ => booking.Service.ToString()
                    };

                    Console.WriteLine($"Kund: {booking.Name.FirstName} {booking.Name.LastName}");
                    Console.WriteLine($"Fordon: {booking.Vehicle.RegistrationNumber}");
                    Console.WriteLine($"Datum och tid: {booking.BookingDate:yyyy-MM-dd HH:mm}");
                    Console.WriteLine($"Tjänst: {serviceSwedish}");
                    Console.WriteLine();
                }

            }

            Console.WriteLine("Tryck på valfri tangent för att gå tillbaka till menyn");
            Console.ReadKey();
        }
    }
}
