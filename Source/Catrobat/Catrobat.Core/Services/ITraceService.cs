using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Annotations;

namespace Catrobat.IDE.Core.Services
{
    public enum TraceType { Info, Warining, Error }

    public interface ITraceService
    {
        List<TraceEntry> Entries { get; set; }

        void Add(TraceType type, string title);

        void Add(TraceType type, string title, string description);

        void Add(TraceType type, string title, string description, string content);

        void Add(TraceEntry entry);

        Task SaveLocal();

        Task LoadLocal();

        string CreateTraceString();
    }

    public class TraceEntry
    {
        public TraceType Type { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            string output = "";

            switch (Type)
            {
                case TraceType.Info:
                    output += "Info: ";
                    break;
                case TraceType.Warining:
                    output += "Warining: ";
                    break;
                case TraceType.Error:
                    output += "Error: ";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (Title != null)
            {
                output += Title + Environment.NewLine;
            }

            if (Description != null)
            {
                output += Description + Environment.NewLine;
            }
                

            if (Content != null)
            {
                output += Content + Environment.NewLine;
            }
                

            return output;
        }
    }
}
