using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{

    //TODO:do we still need it?
    public class XmlIfLogicElseBrickReference : XmlObjectNode
    {
        private string _reference;

        public XmlIfLogicElseBrick IfLogicElseBrick { get; set; }

        public XmlIfLogicElseBrickReference()
        {
        }

        public XmlIfLogicElseBrickReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("ifElseBrick");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(IfLogicElseBrick == null)
                IfLogicElseBrick = ReferenceHelper.GetReferenceObject(this, _reference) as XmlIfLogicElseBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}