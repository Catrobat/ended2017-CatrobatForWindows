using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catrobat.IDE.Core.VersionConverter
{
    public class VersionConverterResult
    {
        public string Xml { get; set; }

        public CatrobatVersionConverter.VersionConverterError Error { get; set; }
    }
}
