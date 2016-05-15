using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters
{
    public class SpriteReferenceConverter : XmlModelConverter<XmlSpriteReference, Sprite>
    {
        public SpriteReferenceConverter() { }

        public override Sprite Convert(XmlSpriteReference o, XmlModelConvertContext c)
        {
            throw new NotImplementedException();
        }

        public override XmlSpriteReference Convert(Sprite m, XmlModelConvertBackContext c)
        {
            throw new NotImplementedException();
        }
    }
}
