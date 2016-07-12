using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    public partial class XmlBroadcastScript : XmlScript, IBroadcastScript
    {
        #region NativeInterface
        public string Name { get { return string.Empty; } set { } }

        public string ReceivedMessage { get; set; }

        #endregion


        public XmlBroadcastScript() { }

        public XmlBroadcastScript(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null && xRoot.Element(XmlConstants.ReceivedMessage) != null)
            {
                ReceivedMessage = xRoot.Element(XmlConstants.ReceivedMessage).Value;
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Script);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlBroadcastScriptType);

            CreateCommonXML(xRoot);

            if (ReceivedMessage != null)
            {
                xRoot.Add(new XElement(XmlConstants.ReceivedMessage)
                {
                    Value = ReceivedMessage
                });
            }

            return xRoot;
        }
    }
}
