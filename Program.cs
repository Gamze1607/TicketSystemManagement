namespace TicketSystemManagement
{
    public class Program
    {
        private static Data data = new Data();
        public static void Main(string[] args)
        {
            SetIOEncoding();

            DisplayMenu();

            string choice;
            while ((choice = Console.ReadLine()) != "x")
            {
                switch (choice)
                {
                    case "1":
                        Event newEvent = DisplayAddEvent();
                        data.Events.Add(newEvent);
                        data.Save();
                        break;
                    case "2":
                        DisplayTicketSales();

                        break;
                    case "3":
                        DisplayTicketAvailability();
                        break;
                    case "4":
                        DisplayAllEvents();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void SetIOEncoding()
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        private static Event DisplayAddEvent()
        {
            throw new NotImplementedException();
        }

        private static void DisplayTicketSales()
        {
            throw new NotImplementedException();
        }

        private static void DisplayTicketAvailability()
        {
            throw new NotImplementedException();
        }
        private static void DisplayAllEvents()
        {
            Console.WriteLine("Show all events:");
            foreach (var ev in data.Events)
            {
                Console.WriteLine($"ID: {ev.eventId}, Name: {ev.name}, Date: {ev.date.ToShortDateString()}, Venue: {ev.venue}, Available Tickets: {ev.availableTickets}, Price:{ev.price:C}");
            }
        }
        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("==================[ М Е Н Ю ]==================");
            Console.WriteLine("|  [1]  Добавяне на ново събитие              |");
            Console.WriteLine("|  [2]  Продажба на билети за събитие         |");
            Console.WriteLine("|  [3]  Проверка за наличността на билети     |");
            Console.WriteLine("|  [4]  Справка за всички събития             |");
            Console.WriteLine("|  [x]  Изход                                 |");
            Console.WriteLine("===============================================");
            Console.Write("> Изберете опция: ");
        }
}   }
