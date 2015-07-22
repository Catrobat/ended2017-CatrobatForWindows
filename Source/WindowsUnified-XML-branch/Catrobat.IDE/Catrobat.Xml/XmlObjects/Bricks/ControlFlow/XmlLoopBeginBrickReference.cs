using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public class XmlLoopBeginBrickReference : XmlObjectNode
    {
        private string _reference;

        public XmlLoopBeginBrick LoopBeginBrick { get; set; }

        public XmlLoopBeginBrickReference() 
        {
        }


        public XmlLoopBeginBrickReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("loopBeginBrick");
            var xRoot = new XElement("brick");
            xRoot.SetAttributeValue("type", "loopBeginBrick");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(LoopBeginBrick == null)
                LoopBeginBrick = ReferenceHelper.GetReferenceObject(this, _reference) as XmlLoopBeginBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}