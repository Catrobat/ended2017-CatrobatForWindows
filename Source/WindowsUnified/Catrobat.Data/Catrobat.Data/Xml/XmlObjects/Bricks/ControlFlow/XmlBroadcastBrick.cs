using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlBroadcastBrick : XmlBrick
    {
        public string BroadcastMessage { get; set; }

        public XmlBroadcastBrick() {}

        public XmlBroadcastBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("broadcastMessage") != null)
            {
                BroadcastMessage = xRoot.Element("broadcastMessage").Value;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("broadcastBrick");

            if (BroadcastMessage != null)
            {
                xRoot.Add(new XElement("broadcastMessage")
                {
                    Value = BroadcastMessage
                });
            }

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}