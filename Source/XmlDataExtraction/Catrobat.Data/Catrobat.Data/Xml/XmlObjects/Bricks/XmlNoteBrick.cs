using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks
{
    public partial class XmlNoteBrick : XmlBrick
    {
        public string Note { get; set; }

        public XmlNoteBrick() {}

        public XmlNoteBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("note") != null)
            {
                Note = xRoot.Element("note").Value;
            }
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("noteBrick");

            if (Note != null)
            {
                xRoot.Add(new XElement("note")
                {
                    Value = Note
                });
            }
            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}