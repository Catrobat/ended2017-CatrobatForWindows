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
