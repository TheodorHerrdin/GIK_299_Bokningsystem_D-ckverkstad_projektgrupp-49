using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TireBookingSystem
{
    public class BookingSystem
    {
        //Den centrala listan som sparar bokningarna
        private static List<Booking> BookingList = new List<Booking>();
            
        //Metoden för att söka efter vilka lediga tider som finns på ett valt datum
        public static void SearchFreeTimes()
        {
            Console.Clear();
            Console.WriteLine("Se vilka tider som är lediga en specifik dag");
            Console.WriteLine();

            //Variabler för att hantera loopen och det valda datumet
            bool isSearchDateValid = false;
            DateTime selectedDate = DateTime.Now;

            //En while-loop som fortsätter tills användaren har angett ett giltigt datum
            while (!isSearchDateValid)
            {
                Console.Write("Vilket datum vill du se tider för? ange (ÅÅÅÅ-MM-DD): ");
                string input = Console.ReadLine();

                //Kontrollerar om inmatningen är ett korrekt datumformat
                if (DateTime.TryParse(input, out selectedDate))
                {
                    //Kontrollerar om det valda datumet har passerat
                    if (selectedDate.Date >= DateTime.Today)
                    {
                        //Om datumet är giltigt sätts boolen till true för att bryta loopen
                        isSearchDateValid = true;
                    }
                    else
                    {
                        //Felmeddelande om användaren anger ett datum som redan har passerat
                        Console.WriteLine("\nFel, du kan inte söka på ett datum som redan har passerat");
                        Console.WriteLine();
                    }
                }
                else
                {
                    //Felmeddelande om användaren anger något som inte kan tolkas som ett datum
                    Console.WriteLine("\nOgiltigt datumformat, använd (ÅÅÅÅ-MM-DD)");
                    Console.WriteLine();
                }
            }

            //Listor för verkstadens bokningsbara tider med timmar och minuter
            List<int> openingHours = new List<int> { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
            List<int> minutes = new List<int> { 0, 30 };

            Console.WriteLine();
            Console.WriteLine($"Lediga tider för {selectedDate:yyyy-MM-dd}:");
            Console.WriteLine();

            //En nästlad foreach-loop för att gå igenom varje timme och minut för det valda datumet
            foreach (int hour in openingHours)
            {
                foreach (int min in minutes)
                {
                    //Här kontrolleras det om tiden är bokad genom att jämföra datum och tid med bokningslistan
                    bool isOccupied = BookingList.Exists(b => b.BookingDate.Date == selectedDate.Date && b.BookingDate.Hour == hour && b.BookingDate.Minute == min);

                    //Formaterar utskriften av tiden snyggt
                    string timeString = $"{hour:D2}:{min:D2}";

                    //Skriver ut om tiden är ledig eller bokad
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

            Console.WriteLine("\nTryck på en valfri tangent för att gå vidare");
            Console.ReadKey();
        }

        //Metoden för att samla in data och lägga till en ny bokning
        public static void AddBooking()
        {
            Console.Clear();
            Console.WriteLine("Ny tidsbokning");
            Console.WriteLine();

            //Läser in användardata som för och efternamn samt registreringsnummer
            Console.Write("Ange förnamn: ");
            string firstName = Console.ReadLine();
            Console.Write("Ange efternamn: ");
            string lastName = Console.ReadLine();
            Console.Write("Ange registreringsnummer: ");
            string registrationNumber = Console.ReadLine().ToUpper();

            //En while-loop tillsammans med en bool för att säkerställa att användaren väljer ett giltigt tjänstealternativ (1-4) innan loopen bryts
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
                    //Väljer användern case 1 så väljs tjänsten TireChange och SelectedService sätts till true för att bryta loopen
                    case "1":
                        selectedService = ServiceType.TireChange;
                        isServiceValid = true;
                        break;
                    //Väljer användern case 2 så väljs tjänsten Balancing och SelectedService sätts till true för att bryta loopen
                    case "2":
                        selectedService = ServiceType.Balancing;
                        isServiceValid = true;
                        break;
                    //Väljer användern case 3 så väljs tjänsten Tirestorage och SelectedService sätts till true för att bryta loopen
                    case "3":
                        selectedService = ServiceType.Tirestorage;
                        isServiceValid = true;
                        break;
                    //Väljer användern case 4 så väljs tjänsten Puncturerepair och SelectedService sätts till true för att bryta loopen
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

            //En while-loop för datumhantering som fortsätter tills ett giltigt datum och tid har angetts och är tillgängligt
            while (!isDateValid)
            {
                Console.WriteLine();
                Console.WriteLine("Ange datum och tid för din bokning verkstaden är öppen mellan 08:00-18:00");
                Console.WriteLine();

                Console.Write("Ange ett datum (ÅÅÅÅ-MM-DD): ");
                string dateInput = Console.ReadLine();
                //Split metoden delar upp datumsträngen i år, månad och dag med -
                string[] dateParts = dateInput.Split('-');

                //Här kontrolleras att användaren anger år, månad och dag korrekt
                if (dateParts.Length == 3 &&
                     int.TryParse(dateParts[0], out year) &&
                     int.TryParse(dateParts[1], out month) &&
                     int.TryParse(dateParts[2], out day))

                {
                    Console.Write("Ange en tid mellan (08:00-17:30): ");
                    string timeInput = Console.ReadLine();
                    //Split metoden delar upp tidssträngen i timme och minut med :
                    string[] timeParts = timeInput.Split(':');

                    //Här kontrolleras att användaren anger timme och minut korrekt
                    if (timeParts.Length == 2 &&
                        int.TryParse(timeParts[0], out hour) &&
                        int.TryParse(timeParts[1], out minute))
                    {
                        //Här kontrolleras att tiden ligger inom verkstadens öppettider
                        if (hour >= 8 && hour <= 17 && (minute == 0 || minute == 30))
                        {
                            try
                            {
                                //Här skapas ett DateTime objekt med de angivna värdena
                                date = new DateTime(year, month, day, hour, minute, 0);

                                //Kontrollerar att datumet inte har passerat
                                if (date < DateTime.Now)
                                {
                                    Console.WriteLine("\nFel, du kan inte boka en tid på ett datum som redan har passerat");
                                    continue;
                                }

                                //Any-metoden används för att kontrollera om den valda tiden redan är bokad
                                bool isOccupied = BookingList.Any(b => b.BookingDate == date);

                                if (isOccupied)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("\nTyvärr är den här tiden redan bokad");
                                    continue;
                                }

                                //När vi har kommit hit så är datumet och tiden giltiga och tillgängliga
                                isDateValid = true;
                                break;

                            }
                            //Felhantering för ogiltiga datum
                            catch
                            {
                                Console.WriteLine();
                                Console.WriteLine("\nOgiltigt datum kontrollera att året, månaden och dagen stämmer.");
                            }
                        }
                        //Felhantering för tider utanför öppettiderna
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("\nTiden måste vara mellan 08:00 och 17:30. Försök igen.");
                        }
                    }
                    //Felhantering för ogiltiga tidsformat
                    else
                    {
                        Console.WriteLine("\nOgiltigt tidsformat, använd (HH:mm)");
                    }
                }
                //Felhantering för ogiltiga datumformat
                else
                {
                    Console.WriteLine("\nOgiltigt datumformat, använd (ÅÅÅÅ-MM-DD)");
                }
            }

            //Här skapas nya objekt av klasserna CustomerName och VehicleInformation med de insamlade uppgifterna
            CustomerName customerName = new CustomerName(firstName, lastName);
            VehicleInformation vehicleInfo = new VehicleInformation(registrationNumber);

            //Skapar en ny bokning med de insamlade uppgifterna
            Booking newBooking = new Booking(customerName, vehicleInfo, date, selectedService);

            //Den nya bokningen sparas till bokningslistan så den finns kvar under programmets gång
            BookingList.Add(newBooking);

            Console.WriteLine();
            Console.WriteLine("Bokningen är nu registrerad!");
            Console.WriteLine("\nTryck på en valfri tangent för att gå vidare");
            Console.ReadKey();
        }

        //Metod för att avboka en befintlig bokning
        public static void CancelBooking()
        {
            Console.Clear();
            Console.WriteLine("Avboka tid");
            Console.WriteLine();

            Console.Write("Ange fordonets registreringsnummer för att avboka: ");
            string SearchRegistrationNumber = Console.ReadLine().ToUpper();

            //Söker efter den första bokningen som matchar det angivna registreringsnumret
            Booking? bookingToRemove = BookingList.Find(b => b.Vehicle.RegistrationNumber == SearchRegistrationNumber);

            //Om bokningen hittas så visas information om bokningen och användaren får bekräfta avbokningen
            if (bookingToRemove != null)
            {
                Console.WriteLine("Hittade bokning för registreringsnummer: " + SearchRegistrationNumber);
                Console.WriteLine($"Kund: {bookingToRemove.Name.FirstName} {bookingToRemove.Name.LastName}");
                Console.WriteLine($"Datum och tid: {bookingToRemove.BookingDate:yyyy-MM-dd HH:mm}");

                Console.WriteLine();
                Console.Write("För att avboka tiden vänligen ange (j) om du vill behålla tiden ange (n): ");
                string confirm = Console.ReadLine().ToLower();

                //Om användaren bekräftar avbokningen med (j) så tas bokningen bort från listan
                if (confirm == "j")
                {
                    //Remove tar bort den specifika bokningen från listan
                    BookingList.Remove(bookingToRemove);
                    Console.WriteLine("\nBokningen har nu tagits bort");
                }
                //Om användaren avbryter avbokningen med (n) så visas ett meddelande om att avbokningen är avbruten
                else
                {
                    Console.WriteLine("\nAvbokningen är avbruten");
                }
            }
            //Om ingen bokning hittas med det angivna registreringsnumret så visas ett felmeddelande
            else
            {
                Console.WriteLine("\nIngen bokning hittades för registreringsnummer: " + SearchRegistrationNumber);
            }

            Console.WriteLine("\nTryck på valfri tangent för att gå vidare");
            Console.ReadKey();
        }

        //Metod för att lista alla befintliga bokningar
        public static void ListBookings()
        {
            Console.Clear();
            Console.WriteLine("Alla bokningar");
            Console.WriteLine();

            //Kontrollerar om bokningslistan är tom
            if (BookingList.Count == 0)
            {
                Console.WriteLine("Det finns inga bokningar registrerade");
            }
            else
            {
                //En foreach-loop för att gå igenom varje bokning i listan och skriva ut dess detaljer
                foreach (var booking in BookingList)
                {
                    //En switch-sats för att översätta tjänstetyperna från engelska till svenska när de skrivs ut i konsolen
                    string serviceSwedish = booking.Service switch
                    {
                        ServiceType.TireChange => "Däckbyte",
                        ServiceType.Balancing => "Balansering",
                        ServiceType.Tirestorage => "Däckförvaring",
                        ServiceType.Puncturerepair => "Punkteringslagning",
                        _ => booking.Service.ToString()
                    };

                    //Skriver ut bokningsinformationen i ett läsbart format
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
