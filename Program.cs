namespace TicketSystemManagement
{
    public class Program
    {
        private static Data data = new Data();
        public static void Main(string[] args)
        {
            SetIOEncoding();

            DisplayMenu();

            string? choice;
            while (!string.IsNullOrEmpty(choice = Console.ReadLine()))
            {
                if (choice == "x")
                    break;

                switch (choice)
                {
                    case "1":
                        Event newEvent = DisplayAddEvent();
                        data.Events.Add(newEvent);
                        data.Save();
                        break;
                    case "2":
                        DisplayTicketSales(data.Events);
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

        private static void DisplayTicketSales(List<Event> events)
        {
            Console.WriteLine("[2]    Продажба на билети за събитие:    ");
            if (events.Count == 0)
            {
                Console.WriteLine("Няма налични събития.");
                return;
            }

            Console.Write("Въведете ID на събитието от което искате да закупите билети: ");
            if (!int.TryParse(Console.ReadLine(), out int eventIndex) || eventIndex < 1 || eventIndex > events.Count)
            {
                Console.WriteLine("Няма такова събитие.");
                return;
            }

            Event selectedEvent = events[eventIndex - 1];

            Console.Write($"Колко билета искате да закупите за'{selectedEvent.name}'? ");
            if (!int.TryParse(Console.ReadLine(), out int ticketsToSell) || ticketsToSell < 1)
            {
                Console.WriteLine("Броят на билетите е невалиден.");
                return;
            }

            if (ticketsToSell > selectedEvent.availableTickets)
            {
                Console.WriteLine("Няма достатъчно билети.");
                return;
            }

            selectedEvent.availableTickets -= ticketsToSell;
            Console.WriteLine($"{ticketsToSell} билети продадени за '{selectedEvent.name}'. Налични билети след продажба: {selectedEvent.availableTickets}");
        }

        private static void DisplayTicketAvailability()
        {
            Console.WriteLine("[3]  Проверка за наличността на билети:  ");
            throw new NotImplementedException();
        }
        private static void DisplayAllEvents()
        {
            Console.WriteLine("[4]     Справка за всички събития:       ");
            Console.WriteLine("eventID\\name\\date\\venue\\totalTickets\\avaibleTickets\\price");
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

        private static void BackToMenu(string message, bool success = true)
        {
            if (success)
            {
                Console.WriteLine(message);
            }
            Console.WriteLine();
            Console.Write("Натиснете бутона за да се върнете към меню ");
            Console.ReadLine();
            DisplayMenu();
        }
}   }      
