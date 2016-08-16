using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    public partial class XmlStopAllSoundsBrick : XmlBrick
    {
        public XmlStopAllSoundsBrick() {}

        public XmlStopAllSoundsBrick(XElement xElement) : base(xElement) {}

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlStopAllSoundsBrick b = obj as XmlStopAllSoundsBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b);
        }

        public bool Equals(XmlStopAllSoundsBrick b)
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
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlStopAllSoundsBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}
