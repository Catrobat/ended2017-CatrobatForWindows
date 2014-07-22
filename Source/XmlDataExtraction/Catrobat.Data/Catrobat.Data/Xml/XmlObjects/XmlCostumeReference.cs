using System.Xml.Linq;
using Catrobat.Data.Utilities.Helpers;

namespace Catrobat.Data.Xml.XmlObjects
{
    public class XmlCostumeReference : XmlObject
    {
        private string _reference;

        public XmlCostume Costume { get; set; }

        public XmlCostumeReference()
        {
        }

        public XmlCostumeReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        public override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;

        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("look");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public override void LoadReference()
        {
            if(Costume == null)
                Costume = ReferenceHelper.GetReferenceObject(this, _reference) as XmlCostume;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}