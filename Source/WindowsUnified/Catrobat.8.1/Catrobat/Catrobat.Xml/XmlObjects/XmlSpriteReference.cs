using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlSpriteReference : XmlObjectNode
    {
        private string _reference;

        public XmlSprite Sprite { get; set; }

        public override bool Equals(System.Object obj)
        {
            XmlSpriteReference r = obj as XmlSpriteReference;
            if ((object)r == null)
            {
                return false;
            }

            return this.Equals(r);
        }

        public bool Equals(XmlSpriteReference r)
        {
            return _reference.Equals(r._reference);// && Sprite.Equals(r.Sprite);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ _reference.GetHashCode()
                ;//^ Sprite.GetHashCode();
        }

        public XmlSpriteReference()
        {
        }

        public XmlSpriteReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                _reference = xRoot.Attribute(XmlConstants.Reference).Value;
            }
            //Sprite = ReferenceHelper.GetReferenceObject(this, _reference) as Sprite;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Object);

            xRoot.Add(new XAttribute(XmlConstants.Reference, ReferenceHelper.GetReferenceString(this)));

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
