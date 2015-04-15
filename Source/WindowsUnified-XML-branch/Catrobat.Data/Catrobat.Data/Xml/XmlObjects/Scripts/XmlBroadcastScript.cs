using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Scripts
{
    public partial class XmlBroadcastScript : XmlScript
    {
        public string ReceivedMessage { get; set; }

        public XmlBroadcastScript() {}

        public XmlBroadcastScript(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("receivedMessage") != null)
            {
                ReceivedMessage = xRoot.Element("receivedMessage").Value;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("broadcastScript");

            CreateCommonXML(xRoot);

            if (ReceivedMessage != null)
            {
                xRoot.Add(new XElement("receivedMessage")
                {
                    Value = ReceivedMessage
                });
            }

            return xRoot;
        }
    }
}