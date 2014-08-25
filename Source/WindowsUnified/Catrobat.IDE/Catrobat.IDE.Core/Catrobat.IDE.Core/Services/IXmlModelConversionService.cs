using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.Services
{
    public interface IXmlModelConversionService
    {
        void ResetContext();

        ModelBase Convert(XmlObject o);

        XmlObject Convert(ModelBase m);
    }
}
