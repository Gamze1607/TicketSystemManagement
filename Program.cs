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
            throw new NotImplementedException();
        }
        private static void DisplayMenu()
        {
            throw new NotImplementedException();
        }
}   }
