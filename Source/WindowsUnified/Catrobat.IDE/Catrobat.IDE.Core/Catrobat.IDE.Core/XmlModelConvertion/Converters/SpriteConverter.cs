using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters
{
    public class SpriteConverter : XmlModelConverter<XmlSprite, Sprite>
    {
        public override Sprite Convert(XmlSprite o)
        {
            throw new NotImplementedException("TODO: use code from Xml/Converter/XmlObjects/XmlSprite");
        }

        public override XmlSprite Convert(Sprite m)
        {
            throw new NotImplementedException("TODO: use code from Xml/Converter/Models/Sprite");
        }
    }
}
