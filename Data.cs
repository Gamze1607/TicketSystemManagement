using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystemManagement               //by Dzheyda
{
    using System.Text.Json;
    using static Constants;
    public class Data
    {
        public List<Event> Events { get; private set; } = new List<Event>();

        private StreamReader? reader;
        private StreamWriter? writer;

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
            if (!File.Exists(filePath))
            {
                Console.WriteLine($" Стартирай с празен лист за събития.");
                return;
            }
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
