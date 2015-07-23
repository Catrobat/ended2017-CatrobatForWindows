using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlSpriteReference : XmlObjectNode
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

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute(XmlConstants.Reference).Value;
            //Sprite = ReferenceHelper.GetReferenceObject(this, _reference) as Sprite;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Object);

            xRoot.Add(new XAttribute(XmlConstants.Reference, ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(Sprite == null)
                Sprite = ReferenceHelper.GetReferenceObject(this, _reference) as XmlSprite;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}