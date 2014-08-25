using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters
{
    public class SpriteConverter : XmlModelConverter<XmlSprite, Sprite>
    {
        public SpriteConverter(IXmlModelConversionService converter) : base(converter)
        {
        }

        public override Sprite Convert(XmlSprite o, XmlModelConvertContext c)
        {
            throw new NotImplementedException("TODO: use code from Xml/Converter/XmlObjects/XmlSprite");
        }

        public override XmlSprite Convert(Sprite m, XmlModelConvertBackContext c)
        {
            throw new NotImplementedException("TODO: use code from Xml/Converter/Models/Sprite");
        }
    }
}
