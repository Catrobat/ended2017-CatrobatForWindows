using Catrobat_Player.NativeComponent;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlShowBrick : XmlBrick, IShowBrick
    {
        #region NativeInterface

        #endregion

        public XmlShowBrick() {}

        public XmlShowBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlShowBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}
