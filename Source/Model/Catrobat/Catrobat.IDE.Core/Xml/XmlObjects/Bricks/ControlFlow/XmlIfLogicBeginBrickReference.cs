using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public class XmlIfLogicBeginBrickReference : XmlObject
    {
        private string _reference;

        public XmlIfLogicBeginBrick IfLogicBeginBrick { get; set; }

        public XmlIfLogicBeginBrickReference()
        {
        }

        public XmlIfLogicBeginBrickReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("ifBeginBrick");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(IfLogicBeginBrick == null)
                IfLogicBeginBrick = ReferenceHelper.GetReferenceObject(this, _reference) as XmlIfLogicBeginBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}