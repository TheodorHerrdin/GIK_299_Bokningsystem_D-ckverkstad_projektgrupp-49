namespace TireBookingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool runProgram = true;

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

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Booking.SearchFreeTimes();
                        break;
                    case "2":
                        Booking.AddBooking();
                        break;
                    case "3":
                        Booking.CancelBooking();
                        break;
                    case "4":
                        Booking.ListBookings();
                        break;
                    case "5":
                        runProgram = false;
                        break;
                    default:
                        Console.WriteLine("\nOgilitgt val, vänligen ange 1, 2, 3, 4 eller 5... Prova igen");
                        Console.WriteLine("\nTryck på en valfri tagent för att gå tillbaka till menyn");
                        Console.ReadKey();
                        break;

                }
            }
        }
    }
}
