using System.Xml.Linq;
using Catrobat.Data.Utilities.Helpers;

namespace Catrobat.Data.Xml.XmlObjects.Variables
{
    public class XmlUserVariableReference : XmlObject
    {
        private string _reference;

        public XmlUserVariable UserVariable { get; set; }

        public XmlUserVariableReference()
        {
        }

        public XmlUserVariableReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        public override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("userVariable");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public override void LoadReference()
        {
            if(UserVariable == null)
                UserVariable = ReferenceHelper.GetReferenceObject(this, _reference) as XmlUserVariable;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}
