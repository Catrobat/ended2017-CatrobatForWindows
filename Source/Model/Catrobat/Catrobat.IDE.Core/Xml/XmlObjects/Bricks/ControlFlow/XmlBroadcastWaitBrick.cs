using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlBroadcastWaitBrick : XmlBrick
    {
        public string BroadcastMessage { get; set; }

        public XmlBroadcastWaitBrick() {}

        public XmlBroadcastWaitBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("broadcastMessage") != null)
            {
                BroadcastMessage = xRoot.Element("broadcastMessage").Value;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("broadcastWaitBrick");

            if (BroadcastMessage != null)
            {
                xRoot.Add(new XElement("broadcastMessage")
                {
                    Value = BroadcastMessage
                });
            }

            ////CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}