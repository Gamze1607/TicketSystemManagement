using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystemManagement
{
    public class Event
    {
        public int eventId { get; set; }

        public string name { get; set; }

        public DateTime date { get; set; }

        public string venue { get; set; }

        public int totalTickets { get; set; }

        public int availableTickets { get; set; }

        public decimal price { get; set; }

        public Event(int eventId, string name, DateTime date, string venue, int totalTickets, decimal price)
        {
            this.eventId = eventId;
            this.name = name;
            this.date = date;
            this.venue = venue;
            this.totalTickets = totalTickets;
            this.availableTickets = totalTickets; 
            this.price = price;
        }
    }
}
