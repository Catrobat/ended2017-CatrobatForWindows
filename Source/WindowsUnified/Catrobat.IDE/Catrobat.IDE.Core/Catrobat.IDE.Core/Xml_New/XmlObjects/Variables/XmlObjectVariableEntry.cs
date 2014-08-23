using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public class XmlObjectVariableEntry : XmlObjectNode
    {
        internal XmlSpriteReference XmlSpriteReference { get; set; }

        public XmlSprite Sprite
        {
            get
            {
                if (XmlSpriteReference == null)
                {
                    return null;
                }

                return XmlSpriteReference.Sprite;
            }
            set
            {
                if (XmlSpriteReference == null)
                    XmlSpriteReference = new XmlSpriteReference();

                if (XmlSpriteReference.Sprite == value)
                    return;

                XmlSpriteReference.Sprite = value;

                if (value == null)
                    XmlSpriteReference = null;
            }
        }

        public XmlUserVariableList VariableList { get; set; }

        public XmlObjectVariableEntry() { VariableList = new XmlUserVariableList(); }

        public XmlObjectVariableEntry(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            XmlSpriteReference = new XmlSpriteReference(xRoot.Element("object"));
            VariableList = new XmlUserVariableList(xRoot.Element("list"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("entry");

            xRoot.Add(XmlSpriteReference.CreateXml());
            xRoot.Add(VariableList.CreateXml());

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(XmlSpriteReference != null && XmlSpriteReference.Sprite == null)
                XmlSpriteReference.LoadReference();
        }
    }
}
