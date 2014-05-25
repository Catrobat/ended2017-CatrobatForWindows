using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlSpeakBrick : XmlBrick
    {
        public string Text { get; set; }

        public XmlSpeakBrick() {}

        public XmlSpeakBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("text") != null)
            {
                Text = xRoot.Element("text").Value;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("speakBrick");

            if (Text != null)
            {
                xRoot.Add(new XElement("text")
                {
                    Value = Text
                });
            }

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}