using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks
{
    public partial class XmlNoteBrick : XmlBrick
    {
        public string Note { get; set; }

        public XmlNoteBrick() {}

        public XmlNoteBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //if (xRoot.Element("note") != null)
            if (xRoot.Element(XmlConstants.Note) != null)
            {
                //Note = xRoot.Element("note").Value;
                Note = xRoot.Element(XmlConstants.Note).Value;
            }
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("noteBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "noteBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlNoteBrickType);

            if (Note != null)
            {
                //xRoot.Add(new XElement("note")
                xRoot.Add(new XElement(XmlConstants.Note)
                {
                    Value = Note
                });
            }
            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}