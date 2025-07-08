using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystemManagement
{
    public class Event     //by Gamze
    {
        public int eventId { get; set; }

        public string name { get; set; }

        public DateTime date { get; set; }

        public string venue { get; set; }

        public int totalTickets { get; set; }
        
        public int availableTickets { get; set; }
        public decimal price { get; set; }

        public Event(int id, string Name, DateTime Date, string Venue, int totaltickets, decimal Price)
        {
            eventId = id;
            name = Name;
            date = Date;
            venue= Venue;
            totalTickets = totaltickets;
            availableTickets = totaltickets;
            price = Price;
        }
    }
}
