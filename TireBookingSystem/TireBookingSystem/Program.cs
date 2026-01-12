namespace TireBookingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                Console.WriteLine("Nummer 5: Avsluta programmet");
                Console.WriteLine();
                Console.Write("Välj en siffra (1-5): ");

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
                        //Om nummer 5 väljs sätts runProgram till false för att avsluta loopen och därmed programmet
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
    }
}
