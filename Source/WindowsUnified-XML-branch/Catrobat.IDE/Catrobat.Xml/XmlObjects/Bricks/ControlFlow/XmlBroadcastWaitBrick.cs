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
            //if (xRoot.Element("broadcastMessage") != null)
            if (xRoot.Element(XmlConstants.BroadcastMessage) != null)
            {
                //BroadcastMessage = xRoot.Element("broadcastMessage").Value;
                BroadcastMessage = xRoot.Element(XmlConstants.BroadcastMessage).Value;
            }
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("broadcastWaitBrick");
            //var xRoot = new XElement("brick");
            var xRoot = new XElement(XmlConstants.Brick);
            //xRoot.SetAttributeValue("type", "BroadcastWaitBrick");
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlBroadcastWaitBrickType);

            if (BroadcastMessage != null)
            {
                //xRoot.Add(new XElement("broadcastMessage")
                xRoot.Add(new XElement(XmlConstants.BroadcastMessage)
                {
                    Value = BroadcastMessage
                });
            }

            ////CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}