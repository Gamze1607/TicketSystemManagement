using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystemManagement
{
    using System.Text.Json;
    using static Constants;
    public class Data
    {
        public List<Event> Events { get; private set; }

        private StreamReader reader;
        private StreamWriter writer;

        public Data()
        {
            LoadEvents();
        }

        public void Save()
        {
            StreamWriter writer = new StreamWriter(filePath);
            using (writer)
            {
                string jsonData = JsonSerializer.Serialize(Events);
                writer.Write(jsonData);
            }
        }

        public void LoadEvents()
        {
            Events = new List<Event>();
            reader = new StreamReader(filePath);
            using (reader)
            {
                string jsonData = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(jsonData))
                {
                    Events = JsonSerializer.Deserialize<List<Event>>(jsonData)!;
                }
            }
        }

    }
}
