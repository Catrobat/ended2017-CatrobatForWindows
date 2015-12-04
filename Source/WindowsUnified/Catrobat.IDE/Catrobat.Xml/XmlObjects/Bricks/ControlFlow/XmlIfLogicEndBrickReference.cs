using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    //TODO:do we still need it?
    public class XmlIfLogicEndBrickReference : XmlObjectNode
    {
        private string _reference;

        public XmlIfLogicEndBrick IfLogicEndBrick { get; set; }

        public XmlIfLogicEndBrickReference()
        {
        }

        public XmlIfLogicEndBrickReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.XmlIfLogicEndBrick);

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public override void LoadReference()
        {
            if(IfLogicEndBrick == null)
                IfLogicEndBrick = ReferenceHelper.GetReferenceObject(this, _reference) as XmlIfLogicEndBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}