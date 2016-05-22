using System;
using System.Collections.Generic;
using System.Threading;
using Catrobat.IDE.Core.Services.Storage;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Catrobat.IDE.Core.Services.Common
{
    public class TraceService : ITraceService
    {
        private const int MaxTraceEntries = 200;

        public List<TraceEntry> Entries { get; set; }

        public TraceService()
        {
            Entries = new List<TraceEntry>();
        }

        public void Add(TraceType type, string title)
        {
            Add(type, title, null, null);
        }

        public void Add(TraceType type, string title, string description)
        {
            Add(type, title, description, null);
        }

        public void Add(TraceType type, string title, string description, string content)
        {
            var entry = new TraceEntry
            {
                Type = type,
                Title = title,
                Description = description,
                Content = content
            };

            Add(entry);
        }

        public void Add(TraceEntry entry)
        {
            if (Entries.Count >= MaxTraceEntries)
                Entries.RemoveAt(0);

            Entries.Add(entry);
        }

        public async Task SaveLocal()
        {
            using (var storage = ServiceLocator.StorageFactory.CreateStorage(StorageLocation.Local))
            {
                var serializedEntries = JsonConvert.SerializeObject(Entries);
                await storage.WriteTextFileAsync(StorageConstants.TraceFilePath, serializedEntries);
            }
        }

        public async Task LoadLocal()
        {
            using (var storage = ServiceLocator.StorageFactory.CreateStorage(StorageLocation.Local))
            {
                var serializedEntries = await storage.ReadTextFileAsync(StorageConstants.TraceFilePath);

                if (serializedEntries == null)
                    return;

                var entries = JsonConvert.DeserializeObject<List<TraceEntry>>(serializedEntries);

                if (entries != null)
                    Entries = entries;
            }
        }

        public string CreateTraceString()
        {
            var output = "";

            for (int i = Entries.Count - 1; i >= 0; i--)
            {
                output += Entries[i].ToString();

                if (i > 0)
                    output += Environment.NewLine;
            }

            return output;
        }
    }
}
