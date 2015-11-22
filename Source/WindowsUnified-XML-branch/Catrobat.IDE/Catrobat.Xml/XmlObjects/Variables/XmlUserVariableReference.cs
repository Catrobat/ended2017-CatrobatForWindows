using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public class XmlUserVariableReference : XmlObjectNode
    {
        public string _reference;

        public XmlUserVariable UserVariable { get; set; }

        public XElement _xRoot { get; set; }

        public XmlUserVariableReference()
        {
        }

        public XmlUserVariableReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute(XmlConstants.Reference).Value;
            _xRoot = xRoot;
            UserVariable = ReferenceHelper.GetReferenceObject(this, _reference) as XmlUserVariable;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.UserVariable);

            xRoot.Add(new XAttribute(XmlConstants.Reference, ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public override void LoadReference()
        {//TODO: think about it
            if(UserVariable == null)
                UserVariable = ReferenceHelper.GetReferenceObject(this, _reference) as XmlUserVariable;
            if (string.IsNullOrEmpty(_reference)) 
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}
