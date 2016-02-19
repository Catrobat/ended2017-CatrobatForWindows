using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlLookReference : XmlObjectNode
    {
        private string _reference;

        public XmlLook Look { get; set; }

        public XmlLookReference()
        {
        }

        public XmlLookReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
           _reference = xRoot.Attribute(XmlConstants.Reference).Value;

        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Look);

            xRoot.Add(new XAttribute(XmlConstants.Reference, ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public override void LoadReference()
        {
            if(Look == null)
                Look = ReferenceHelper.GetReferenceObject(this, _reference) as XmlLook;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}
