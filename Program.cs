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
                        BackToMenu("Събитието е добавено успешно!", true);
                        break;
                    case "2":
                        DisplayTicketSales(data.Events);
                        data.Save();
                        BackToMenu("Продажбата на билети е завършена успешно!");
                        break;
                    case "3":
                        DisplayTicketAvailability(data.Events);
                        data.Save();
                        BackToMenu("Проверка за наличността на билети е завършена успешно!");
                        break;
                    case "4":
                        DisplayAllEvents();
                        data.Save();
                        BackToMenu("Справката за всички събития е завършена успешно!");
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
            string name;
            do
            {
                Console.Write("Въведете име на събитието: ");
                name = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(name))
                    Console.WriteLine("Името на събитието не може да бъде празно.");
            } while (string.IsNullOrWhiteSpace(name));
            DateTime date;
            while (true)
            {
                Console.Write("Въведете дата на събитието (формат: ДД-ММ-ГГГГ): ");
                if (DateTime.TryParse(Console.ReadLine(), out date))
                {
                    if (date < DateTime.Now)
                        Console.WriteLine("Дата на събитието не може да бъде в миналото.");
                    else
                        break;
                }
                else
                {
                    Console.WriteLine("Невалиден формат на датата.");
                }
            }
            string venue;
            do
            {
                Console.Write("Въведете място на събитието: ");
                venue = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(venue))
                    Console.WriteLine("Мястото на събитието не може да бъде празно.");
            } while (string.IsNullOrWhiteSpace(venue));
            int totalTickets;
            while (true)
            {
                Console.Write("Въведете общ брой билети: ");
                if (int.TryParse(Console.ReadLine(), out totalTickets) && totalTickets > 0)
                    break;
                Console.WriteLine("Общият брой билети трябва да бъде положително число.");
            }
            decimal price;
            while (true)
            {
                Console.Write("Въведете цена на билета: ");
                if (decimal.TryParse(Console.ReadLine(), out price) && price > 0)
                    break;
                Console.WriteLine("Цената на билета трябва да бъде положително число.");
            }
            Console.WriteLine($"Налични билети: {totalTickets}");
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

            Console.WriteLine("{0,-10} {1,-20} {2,-12} {3,-10}", "ID", "Име", "Дата", "Свободни билети");
            foreach (var ev in events)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-12} {3,-10}", ev.eventId, ev.name, ev.date.ToString("ДД-ММ-ГГГГ "), ev.availableTickets);
            }

            Console.Write("Въведете ID на събитието, от което искате да закупите билети: ");
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

        private static void DisplayTicketAvailability(List<Event> events)
        {
            Console.WriteLine("[3]  Проверка за наличността на билети:  ");
            if (events == null || events.Count == 0)
            {
                Console.WriteLine("Няма налични събития.");
                return;
            }

            Console.WriteLine("{0,-10} {1,-20} {2,-12} {3,-10}", "ID", "Име", "Дата", "Свободни билети");
            foreach (var ev in events)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-12} {3,-10}", ev.eventId, ev.name, ev.date.ToString("ДД-ММ-ГГГГ "), ev.availableTickets);
            }

            Console.Write("Въведете ID на събитието,за да видите наличните билети: ");
            if (!int.TryParse(Console.ReadLine(), out int eventIndex) || eventIndex < 1 || eventIndex > events.Count)
            {
                Console.WriteLine("Няма такова събитие.");
                return;
            }

            Event selectedEvent = events[eventIndex - 1];

            if (selectedEvent.availableTickets > 0)
            {
                Console.WriteLine($"'{selectedEvent.name}' има {selectedEvent.availableTickets} налични билети.");
            }
            else
            {
                Console.WriteLine($"Билетите за'{selectedEvent.name}' вече са разпродадени.");
            }

        }
        private static void DisplayAllEvents()
        {
            Console.WriteLine("[4]     Справка за всички събития:       ");
            Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-12} {4,-15} {5,-18} {6,-10}",
                "eventId", "name", "date", "venue", "totalTickets", "availableTickets", "price");
            foreach (var ev in data.Events)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-12} {4,-15} {5,-18} {6,-10:F2}",
                    ev.eventId,
                    ev.name,
                    ev.date.ToString("ДД-ММ-ГГГГ"),
                    ev.venue,
                    ev.totalTickets,
                    ev.availableTickets,
                    ev.price);
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
            Console.Write("Натиснете бутона Enter, за да се върнете към меню ");
            Console.ReadLine();
            DisplayMenu();
        }
}   }      
