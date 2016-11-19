using Catrobat_Player.NativeComponent;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public partial class XmlBroadcastBrick : XmlBrick , IBroadcastBrick
    {
        #region NativeInterface
        public string BroadcastMessage { get; set; }

        #endregion

        public XmlBroadcastBrick() {}

        public XmlBroadcastBrick(XElement xElement) : base(xElement) {}

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlBroadcastBrick b = obj as XmlBroadcastBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b) && this.BroadcastMessage.Equals(b.BroadcastMessage);
        }

        public bool Equals(XmlBroadcastBrick b)
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
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlBroadcastBrickType);
            if (BroadcastMessage != null)
            {
                xRoot.Add(new XElement(XmlConstants.BroadcastMessage)
                {
                    Value = BroadcastMessage
                });
            }

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}
