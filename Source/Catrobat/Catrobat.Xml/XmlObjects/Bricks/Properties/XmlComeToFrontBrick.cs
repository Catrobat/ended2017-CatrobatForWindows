using System.Xml.Linq;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlComeToFrontBrick : XmlBrick//, //IComeToFrontBrick
    {
        public override bool Equals(System.Object obj)
        {
            XmlComeToFrontBrick b = obj as XmlComeToFrontBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b);
        }

        public bool Equals(XmlComeToFrontBrick b)
        {
            return this.Equals((XmlBrick)b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public XmlComeToFrontBrick() {}

        public XmlComeToFrontBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlComeToFrontBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}
