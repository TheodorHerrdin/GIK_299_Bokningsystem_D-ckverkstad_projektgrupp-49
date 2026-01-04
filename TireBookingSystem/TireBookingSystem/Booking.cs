using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireBookingSystem
{
    //Structs för att definiera kundnamn och registeringsnummer
    public struct CustomerName 
    {
        public string FirstName;
        public string LastName;
    }

    public struct VehicleInfo 
    {
        public string RegistrationNumber;
    }

    //Enum för att definiera de olika tjänsterna som verkstaden erbjuder
    public enum ServiceType 
    {
        TireChange = 1,
        Balancing,
        Tirestorage,
        Puncturerepair
    }

    //Huvudklassen för allt som rör bokningarna
    public class Booking
    {
        //Egenskaper som varje bokning måste innehålla
        public CustomerName Name { get; set; }
        public VehicleInfo Vehicle { get; set; }
        public DateTime BookingDate { get; set; }
        public ServiceType Service { get; set; }

        //Den centrala listan som lagrar alla bokningar
        public static List<Booking> BookingList = new List<Booking>();

        //Konstruktorn körs varje gång en ny bokning skapas
        public Booking(CustomerName name, VehicleInfo vehicle, DateTime date, ServiceType service)
        {
            Name = name;
            Vehicle = vehicle;
            BookingDate = date;
            Service = service;
        }

        //Metoden för att söka efter vilka lediga tider som finns på ett valt datum
        public static void SearchFreeTimes() 
        {
            Console.Clear();
            Console.WriteLine();
            Console.Write("Vilket datum vill du se tider för? ange (ÅÅÅÅ-MM-DD): ");

            //Tillsammans med en if-sats för att hantera både giltiga och ogiltiga inmatningar utan att systemet kraschar
            //En TryParse för att säkerställa att användaren matar in ett giltigt datum
            if (DateTime.TryParse(Console.ReadLine(), out DateTime selectedDate)) 
            {
                //Här definieras det vilka tider som verkstaden är öppen och som går att bokas
                List<int> openingHours = new List<int> { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
                List<int> minutes = new List<int> { 0, 30 };
                Console.WriteLine();

                //Här loopas det igenom alla öppettider och kontrolleras mot bokningslistan om tiden är bokad eller ledig
                foreach (int hour in openingHours)
                {
                    foreach (int min in minutes) 
                    {
                        //Här kontrolleras det om tiden är bokad genom att jämföra datum och tid med bokningslistan
                        bool isOccupied = BookingList.Exists(b => b.BookingDate.Date == selectedDate.Date && b.BookingDate.Hour == hour && b.BookingDate.Minute == min);

                        string timeString = $"{hour:D2}:{min:D2}";

                        if (!isOccupied)
                        {
                            Console.WriteLine($"{hour:D2}:{min:D2} - Ledig");
                        }
                        else
                        {
                            Console.WriteLine($"{hour:D2}:{min:D2} - Bokad");
                        }
                    }
                }
            }
            else 
            {
                Console.WriteLine("\nOgiltigt datumformat, använd (ÅÅÅÅ-MM-DD)");
            }

            Console.WriteLine("\nTryck på en valfri tangent för att gå vidare");
            Console.ReadKey();
        }

        //Metoden för att lägga till en ny bokning
        public static void AddBooking()
        {
            Console.Clear();
            Console.WriteLine("Ny tidsbokning");
            Console.WriteLine();

            //Här hämtas uppgifter från användaren som för och efternamn samt registreringsnummer
            Console.Write("Ange förnamn: ");
            string FirstName = Console.ReadLine();
            Console.Write("Ange efternamn: ");
            string LastName = Console.ReadLine();
            Console.Write("Ange registreringsnummer: ");
            string RegistrationNumber = Console.ReadLine();

            //Här använder vi en while-loop för att säkerställa att användaren väljer ett giltigt tjänstealternativ (1-4)
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

                //Här används en switch-sats för att hantera användarens val av tjänst
                switch (serviceChoice)
                {
                    //Väljer användern case 1 så väljs tjänsten TireChange och Selectedservice sätts till true för att bryta loopen
                    case "1":
                        selectedService = ServiceType.TireChange;
                        isServiceValid = true;
                        break;
                    //Väljer användern case 2 så väljs tjänsten Balancing och Selectedservice sätts till true för att bryta loopen
                    case "2":
                        selectedService = ServiceType.Balancing;
                        isServiceValid = true;
                        break;
                    //Väljer användern case 3 så väljs tjänsten Tirestorage och Selectedservice sätts till true för att bryta loopen
                    case "3":
                        selectedService = ServiceType.Tirestorage;
                        isServiceValid = true;
                        break;
                    //Väljer användern case 4 så väljs tjänsten Puncturerepair och Selectedservice sätts till true för att bryta loopen
                    case "4":
                        selectedService = ServiceType.Puncturerepair;
                        isServiceValid = true;
                        break;
                    //Om användaren anger ett ogiltigt val så visas ett felmeddelande och loopen fortsätter
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

                Console.Write("Ange ett datum (ÅÅÅÅ-MM-DD): ");
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
            Console.WriteLine("\nTryck på en valfri tangent för att gå vidare");
            Console.ReadKey();
        }

        public static void CancelBooking() 
        {
            Console.Clear();
            Console.WriteLine("Avboka tid");
            Console.WriteLine();

            Console.Write("Ange fordonets registreringsnummer för att avboka: ");
            Console.WriteLine();
            string SearchRegistrationNumber = Console.ReadLine();

            Booking bookingToRemove = BookingList.Find(b => b.Vehicle.RegistrationNumber == SearchRegistrationNumber);

            if (bookingToRemove != null) 
            {
                Console.WriteLine("Hittade bokning för registreringsnummer: " + SearchRegistrationNumber);
                Console.WriteLine($"Kund: {bookingToRemove.Name.FirstName} {bookingToRemove.Name.LastName}");
                Console.WriteLine($"Datum och tid: {bookingToRemove.BookingDate:yyyy-MM-dd HH:mm}");

                Console.WriteLine();
                Console.Write("För att avboka tiden vänligen ange (j) om du vill behålla tiden ange (n): ");
                string confirm = Console.ReadLine();

                if (confirm == "j") 
                {
                    BookingList.Remove(bookingToRemove);
                    Console.WriteLine("\nBokningen har nu tagits bort");
                }
                else 
                {
                    Console.WriteLine("\nAvbokningen är avbruten");
                }
            }
            else 
            {
                Console.WriteLine("\nIngen bokning hittades för registreringsnummer: " + SearchRegistrationNumber);
            }

            Console.WriteLine("\nTryck på valfri tangent för att gå vidare");
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

            Console.WriteLine("\nTryck på valfri tangent för att gå tillbaka till menyn");
            Console.ReadKey();
        }
    }
}
