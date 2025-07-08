namespace TicketSystemManagement
{
    public class Program
    {
        private static Data data = new Data();
        public static void Main(string[] args)  //by Gamze
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
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        BackToMenu("Събитието е добавено успешно!", true);
                        Console.ResetColor();
                        break;
                    case "2":
                        DisplayTicketSales(data.Events);
                        data.Save();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        BackToMenu("Продажбата на билети е завършена успешно!");
                        Console.ResetColor();
                        break;
                    case "3":
                        DisplayTicketAvailability(data.Events);
                        data.Save();
                        Console.ForegroundColor = ConsoleColor.Green;
                        BackToMenu("Проверка за наличността на билети е завършена успешно!");
                        Console.ResetColor();
                        break;
                    case "4":
                        DisplayAllEvents();
                        data.Save();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        BackToMenu("Справката за всички събития е завършена успешно!");
                        break;
                    default:
                        break;
                }
            }

        }

        private static void SetIOEncoding() //by Gamze
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
        }

        private static Event DisplayAddEvent() //by Gamze, by Dzheyda
                                               //създаване на ново събитие
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[1]     Добавяне на ново събитие        ");
            Console.ResetColor();
            Console.WriteLine("========================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            int id = data.Events.Count > 0 ? data.Events.Max(e => e.eventId) + 1 : 1; //създаване на ID за събитието, като се взема най-голямото ID от съществуващите събития и се увеличава с 1
            string name; //създаване на променлива за име и валидиране на въведената стойност
            do
            {
                Console.Write("Въведете име на събитието: ");
                name = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(name))
                    Console.WriteLine("Името на събитието не може да бъде празно.");
            } while (string.IsNullOrWhiteSpace(name));
            DateTime date; //създаване на променлива за дата и валидиране на въведената стойност
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
            string venue; //създаване на променлива за място и валидиране на въведената стойност
            do
            {
                Console.Write("Въведете място на събитието: ");
                venue = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(venue))
                    Console.WriteLine("Мястото на събитието не може да бъде празно.");
            } while (string.IsNullOrWhiteSpace(venue));
            int totalTickets; //създаване на променлива за общ брой билети и валидиране на въведената стойност
            while (true)
            {
                Console.Write("Въведете общ брой билети: ");
                if (int.TryParse(Console.ReadLine(), out totalTickets) && totalTickets > 0)
                    break;
                Console.WriteLine("Общият брой билети трябва да бъде положително число.");
            }
            decimal price; //създаване на променлива за цена и валидиране на въведената стойност
            while (true)
            {
                Console.Write("Въведете цена на билета: ");
                if (decimal.TryParse(Console.ReadLine(), out price) && price > 0)
                    break;
                Console.WriteLine("Цената на билета трябва да бъде положително число.");
            }
            Console.WriteLine($"Налични билети: {totalTickets}");
            Console.ResetColor();
            return new Event(id, name, date, venue, totalTickets, price); //връща новосъздаденото събитие
        }

        private static void DisplayTicketSales(List<Event> events) //by Dzheyda
            //извеждане за продажбата на билети за събития
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("[2]    Продажба на билети за събитие:    ");
            Console.ResetColor();
            Console.WriteLine("========================================");
            Console.ForegroundColor = ConsoleColor.Magenta;
            if (events.Count == 0) // проверка за налични събития
            {
                Console.WriteLine("Няма налични събития.");
                return;
            }
            Console.WriteLine("Налични събития:");
            for (int i = 0; i < events.Count; i++) // извеждане на списък с налични събития
            {
                var ev = events[i];
                Console.WriteLine($"{i + 1}. Име: {ev.name} | Дата: {ev.date.ToShortDateString()} | Местоположение: {ev.venue}  | Цена: {ev.price:C}");
            }
            Console.ResetColor();
            Console.WriteLine("========================================");
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.Write("Въведете ID на събитието, от което искате да закупите билети: ");
            if (!int.TryParse(Console.ReadLine(), out int eventIndex) || eventIndex < 1 || eventIndex > events.Count) // проверка за валидност на въведеното ID
            {
                Console.WriteLine("Няма такова събитие.");
                return;
            }

            Event selectedEvent = events[eventIndex - 1];

            Console.Write($"Колко билета искате да закупите за'{selectedEvent.name}'? ");
            if (!int.TryParse(Console.ReadLine(), out int ticketsToSell) || ticketsToSell < 1) // проверка за валидност на броя въведени билети
            {
                Console.WriteLine("Броят на билетите е невалиден.");
                return;
            }

            if (ticketsToSell > selectedEvent.availableTickets) // проверка за достатъчно налични билети
            {
                Console.WriteLine("Няма достатъчно билети.");
                return;
            }

            selectedEvent.availableTickets -= ticketsToSell;
            Console.WriteLine($"{ticketsToSell} билети продадени за '{selectedEvent.name}'. Налични билети след продажба: {selectedEvent.availableTickets}");
            Console.ResetColor();
        }

        private static void DisplayTicketAvailability(List<Event> events) //by Gamze
                                                                          //извеждане на наличните билети за събития
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("[3]  Проверка за наличността на билети:  ");
            Console.ResetColor();
            Console.WriteLine("========================================");
            Console.ForegroundColor = ConsoleColor.Green;
            if (events == null || events.Count == 0) //проверка за налични събития
            {
                Console.WriteLine("Няма налични събития.");
                return;
            }
            Console.WriteLine("Налични събития:");
            for (int i = 0; i < events.Count; i++) //извеждане на списък с налични събития
            {
                var ev = events[i];
                Console.WriteLine($"{i + 1}. {ev.name} | Дата: {ev.date.ToShortDateString()} | Местоположение: {ev.venue}  | Цена: {ev.price:C}");
            }

            Console.ResetColor();
            Console.WriteLine("========================================");
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("Въведете ID на събитието,за да видите наличните билети: ");
            if (!int.TryParse(Console.ReadLine(), out int eventIndex) || eventIndex < 1 || eventIndex > events.Count) //проверка за валидност на въведеното ID
            {
                Console.WriteLine("Няма такова събитие.");
                return;
            }

            Event selectedEvent = events[eventIndex - 1];

            if (selectedEvent.availableTickets > 0) //проверка за налични билети
            {
                Console.WriteLine($"'{selectedEvent.name}' има {selectedEvent.availableTickets} налични билети.");
            }
            else //ако няма налични билети
            {
                Console.WriteLine($"Билетите за'{selectedEvent.name}' вече са разпродадени.");
            }
            Console.ResetColor();
        }
        private static void DisplayAllEvents() //by Dzheyda, by Gamze
            //исвеждане на всички събития
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("[4]     Справка за всички събития:       ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("eventID\tname\tdate      \tvenue\ttotalTickets\tavaibleTickets\tprice");
            foreach (var ev in data.Events) //извеждане на информация за събитията
            {
                Console.WriteLine($"{ev.eventId}\t{ev.name}\t{ev.date.ToShortDateString()}\t{ev.venue}\t{ev.totalTickets}        \t{ev.availableTickets}        \t{ev.price:C}");
            }
            Console.ResetColor();
        }
        private static void DisplayMenu() //by Dzheyda
                                          //извеждане на главното меню
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("==================[ М Е Н Ю ]==================");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  [1]  Добавяне на ново събитие                ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("  [2]  Продажба на билети за събитие           ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("  [3]  Проверка за наличността на билети       ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  [4]  Справка за всички събития               ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  [x]  Изход                                   ");
            Console.ResetColor();
            Console.WriteLine("===============================================");
            Console.Write("> Изберете опция: ");
        }

        private static void BackToMenu(string message, bool success = true) //by Dzheyda
                                                                            //връщане към главното меню
        {
            if (success)
            {
                Console.WriteLine(message);
            }
            Console.WriteLine();
            Console.ResetColor();
            Console.Write("Натиснете бутона Enter, за да се върнете към меню ");
            Console.ReadLine();
            DisplayMenu();
        }
}   }      
