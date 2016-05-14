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

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlShowBrick b = obj as XmlShowBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b);
        }

        public bool Equals(XmlShowBrick b)
        {
            return this.Equals((XmlBrick)b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlShowBrickType);

            return xRoot;
        }
    }
}
