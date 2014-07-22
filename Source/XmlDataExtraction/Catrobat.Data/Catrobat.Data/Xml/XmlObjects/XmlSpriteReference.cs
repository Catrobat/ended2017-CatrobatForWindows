using System.Xml.Linq;
using Catrobat.Data.Utilities.Helpers;

namespace Catrobat.Data.Xml.XmlObjects
{
    public class XmlSpriteReference : XmlObject
    {
        private string _reference;

        public XmlSprite Sprite { get; set; }

        public XmlSpriteReference()
        {
        }

        public XmlSpriteReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        public override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            //Sprite = ReferenceHelper.GetReferenceObject(this, _reference) as Sprite;
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("object");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public override void LoadReference()
        {
            if(Sprite == null)
                Sprite = ReferenceHelper.GetReferenceObject(this, _reference) as XmlSprite;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}