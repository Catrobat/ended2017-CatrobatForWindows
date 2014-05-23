using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Scripts
{
    public partial class XmlStartScript : XmlScript
    {
        public XmlStartScript() {}

        public XmlStartScript(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("startScript");

            CreateCommonXML(xRoot);

            return xRoot;
        }

        public override XmlObject Copy()
        {
            var newStartScript = new XmlStartScript();
            if (Bricks != null)
            {
                newStartScript.Bricks = Bricks.Copy() as XmlBrickList;
            }

            return newStartScript;
        }

        public override bool Equals(XmlObject other)
        {
            var otherScript = other as XmlStartScript;

            if (otherScript == null)
                return false;

            return Bricks.Equals(((XmlScript) otherScript).Bricks);
        }
    }
}