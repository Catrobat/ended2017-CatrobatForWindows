using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.CatrobatObjects.Costumes
{
    public class CostumeReference : DataObject
    {
        internal string _reference;

        private Costume _costume;
        public Costume Costume
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


        public CostumeReference()
        {
        }

        public CostumeReference(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;

        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("look");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(Costume == null)
                Costume = ReferenceHelper.GetReferenceObject(this, _reference) as Costume;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public DataObject Copy()
        {
            var newCostumeRef = new CostumeReference();
            newCostumeRef.Costume = _costume;

            return newCostumeRef;
        }

        public override bool Equals(DataObject other)
        {
            var otherReference = other as CostumeReference;

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