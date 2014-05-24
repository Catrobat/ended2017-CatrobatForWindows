using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlCostumeReference : XmlObject
    {
        internal string _reference;

        private XmlCostume _costume;
        public XmlCostume Costume
        {
            get { return _costume; }
            set
            {
                if (_costume == value)
                {
                    return;
                }

                _costume = value;
                RaisePropertyChanged();
            }
        }


        public XmlCostumeReference()
        {
        }

        public XmlCostumeReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;

        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("look");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(Costume == null)
                Costume = ReferenceHelper.GetReferenceObject(this, _reference) as XmlCostume;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public XmlObject Copy()
        {
            var newCostumeRef = new XmlCostumeReference();
            newCostumeRef.Costume = _costume;

            return newCostumeRef;
        }

        public override bool Equals(XmlObject other)
        {
            var otherReference = other as XmlCostumeReference;

            if (otherReference == null)
                return false;

            if (Costume.Name != otherReference.Costume.Name)
                return false;
            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}