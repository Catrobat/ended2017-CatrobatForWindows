using System.Xml.Linq;
using Catrobat.Data.Utilities.Helpers;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public class XmlIfLogicElseBrickReference : XmlObject
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

        public override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("ifElseBrick");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public override void LoadReference()
        {
            if(IfLogicElseBrick == null)
                IfLogicElseBrick = ReferenceHelper.GetReferenceObject(this, _reference) as XmlIfLogicElseBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}