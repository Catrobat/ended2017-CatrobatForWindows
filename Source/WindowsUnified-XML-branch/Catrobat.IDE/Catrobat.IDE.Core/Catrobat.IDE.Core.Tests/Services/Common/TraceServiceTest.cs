using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Tests.SampleData;
using System.IO;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml;

namespace Catrobat.IDE.Core.Tests.Services.Common
{
    public class TraceServiceTest : ITraceService
    {
        public List<TraceEntry> Entries { get; set; }

        public void Add(TraceType type, string title)
        {
            throw new NotImplementedException();
        }

        public void Add(TraceType type, string title, string description = null)
        {
           // Nothing here
        }

        public void Add(TraceType type, string title, string description, string content)
        {
            // Nothing here
        }

        public void Add(TraceEntry entry)
        {
            // Nothing here
        }

        public Task SaveLocal()
        {
            // Nothing here
            return Task.Run(() => { });
        }

        public Task LoadLocal()
        {
            return Task.Run(() => { });
            // Nothing here
        }

        public string CreateTraceString()
        {
            return "";
            // Nothing here
        }
    }

}
