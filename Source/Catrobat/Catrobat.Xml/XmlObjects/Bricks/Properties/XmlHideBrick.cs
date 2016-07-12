using Catrobat_Player.NativeComponent;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlHideBrick : XmlBrick, IHideBrick
    {
        #region NativeInterface

        #endregion

        public override bool Equals(System.Object obj)
        {
            XmlHideBrick b = obj as XmlHideBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b);
        }

        public bool Equals(XmlHideBrick b)
        {
            return this.Equals((XmlBrick)b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public XmlHideBrick() {}

        public XmlHideBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlHideBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}
