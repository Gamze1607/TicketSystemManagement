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
            Console.WriteLine("[1]     Добавяне на ново събитие        ");
            Console.WriteLine("========================================");
            int id = data.Events.Count > 0 ? data.Events.Max(e => e.eventId) + 1 : 1;
            Console.Write("Въведете име на събитието: ");
            string name = Console.ReadLine()!;
            Console.Write("Въведете дата на събитието (формат: ГГГГ-ММ-ДД): ");
            DateTime date = DateTime.Parse(Console.ReadLine()!);
            Console.Write("Въведете място на събитието: ");
            string venue = Console.ReadLine()!;
            Console.Write("Въведете общ брой билети: ");
            int totalTickets = int.Parse(Console.ReadLine()!);
            int availableTickets = totalTickets; 
            Console.Write("Въведете цена на билета: ");
            decimal price = decimal.Parse(Console.ReadLine()!);
            Console.WriteLine("Събитието е добавено успешно!");
            return new Event(id, name, date, venue, totalTickets, price);
        }

        private static void DisplayTicketSales()
        {
            Console.WriteLine("[2]    Продажба на билети за събитие:    ");
            throw new NotImplementedException();
        }

        private static void DisplayTicketAvailability()
        {
            Console.WriteLine("[3]  Проверка за наличността на билети:  ");
            throw new NotImplementedException();
        }
        private static void DisplayAllEvents()
        {
            Console.WriteLine("[4]     Справка за всички събития:       ");
            foreach (var ev in data.Events)
            {
                Console.Write($"ID: {ev.eventId}, Име: {ev.name}, Дата: {ev.date.ToShortDateString()}, Местоположение: {ev.venue}, Общ брой билети: {ev.totalTickets}, Налични билети: {ev.availableTickets}, Цена:{ev.price:C}");
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

        private static void BackToMenu()
        {
            
        }
}   }      
