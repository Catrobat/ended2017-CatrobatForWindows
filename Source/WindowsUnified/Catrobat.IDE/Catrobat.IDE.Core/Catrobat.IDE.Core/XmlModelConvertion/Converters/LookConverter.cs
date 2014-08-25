using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters
{
    public class LookConverter : XmlModelConverter<XmlLook, Look>
    {
        public override Look Convert(XmlLook o, XmlModelConvertContext c)
        {
            return new Look
            {
                Name = o.Name,
                FileName = o.FileName
            };
        }

        public override XmlLook Convert(Look m, XmlModelConvertBackContext c)
        {
            return new XmlLook
            {
                Name = m.Name,
                FileName = m.FileName
            };
        }
    }
}
