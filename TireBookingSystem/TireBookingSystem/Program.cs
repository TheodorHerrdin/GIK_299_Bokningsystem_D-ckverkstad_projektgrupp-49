namespace TireBookingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Anropar metoden AdminLogin för att tvinga administratörsinloggning innan menyn visas
            AdminLogin();
            
            //En bool för att hålla igång programmet tills användaren väljer att avsluta
            bool runProgram = true;

            //En while-loop som visar menyn och hanterar användarens val och som pågår så länge runProgram är true
            while (runProgram)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Theo och Shahos Däckverkstad AB");
                Console.WriteLine();
                Console.WriteLine("Nummer 1: Sök efter lediga tider");
                Console.WriteLine("Nummer 2: Lägg till en ny bokning");
                Console.WriteLine("Nummer 3: Avboka din tid");
                Console.WriteLine("Nummer 4: Visa alla bokningar");
                Console.WriteLine("Nummer 5: Visa dagens bokningar");
                Console.WriteLine("Nummer 6: Avsluta programmet");
                Console.WriteLine();
                Console.Write("Välj en siffra (1-6): ");

                //Läser in användarens val som en sträng
                string choice = Console.ReadLine();

                //En switch-sats för att hantera användarens val och anropa motsvarande metoder i Booking-klassen
                switch (choice)
                {
                    case "1":
                        BookingSystem.SearchFreeTimes();
                        break;
                    case "2":
                        BookingSystem.AddBooking();
                        break;
                    case "3":
                        BookingSystem.CancelBooking();
                        break;
                    case "4":
                        BookingSystem.ListBookings();
                        break;
                    case "5":
                        BookingSystem.ListTodaysBookings();
                        break;
                    //Om nummer 6 väljs sätts runProgram till false för att avsluta loopen och därmed programmet
                    case "6":
                        runProgram = false;
                        break;
                    //Om användern anger något annat än 1-5 visas ett felmeddelande och menyn visas igen
                    default:
                        Console.WriteLine("\nOgiltigt val, vänligen ange 1, 2, 3, 4 eller 5... Prova igen");
                        Console.WriteLine("\nTryck på en valfri tangent för att gå tillbaka till menyn");
                        Console.ReadKey();
                        break;

                }
            }
        }

        //Metod för administratörsinloggning
        static void AdminLogin() 
        {
            //En oändlig loop som fortsätter tills rätt lösenord anges
            while (true) 
            {
                Console.Clear();
                Console.WriteLine("Administratörsinloggning");
                Console.Write("Ange lösenord: ");
                string input = Console.ReadLine();

                //Kontrollerar om det angivna lösenordet är korrekt
                if (input == "admin123")
                {
                    //Om lösenordet är korrekt bryts loopen och programmet fortsätter
                    break;
                }
                //Om lösenordet är felaktigt visas ett felmeddelande och loopen fortsätter
                else
                {
                    Console.WriteLine("Fel lösenord. Tryck på en valfri tangent för att försöka igen.");
                    Console.ReadKey();
                }
            }
        }
    }
}
