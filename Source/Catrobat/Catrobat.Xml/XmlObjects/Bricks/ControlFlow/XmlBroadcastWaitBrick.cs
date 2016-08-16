using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlBroadcastWaitBrick : XmlBrick
    {
        public string BroadcastMessage { get; set; }

        public XmlBroadcastWaitBrick() {}

        public XmlBroadcastWaitBrick(XElement xElement) : base(xElement) {}

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlBroadcastWaitBrick b = obj as XmlBroadcastWaitBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.BroadcastMessage.Equals(b.BroadcastMessage);
        }

        public bool Equals(XmlBroadcastWaitBrick b)
        {
            return this.Equals((XmlBrick)b) && this.BroadcastMessage.Equals(b.BroadcastMessage);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ BroadcastMessage.GetHashCode();
        }
        #endregion

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null && xRoot.Element(XmlConstants.BroadcastMessage) != null)
            {
                BroadcastMessage = xRoot.Element(XmlConstants.BroadcastMessage).Value;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlBroadcastWaitBrickType);

            if (BroadcastMessage != null)
            {
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
