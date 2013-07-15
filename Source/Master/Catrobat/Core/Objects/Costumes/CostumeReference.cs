using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Costumes
{
    public class CostumeReference : DataObject
    {
        private readonly Sprite _sprite;
        private string _reference;

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


        public CostumeReference(Sprite parent)
        {
            _sprite = parent;
        }

        public CostumeReference(XElement xElement, Sprite parent)
        {
            _sprite = parent;
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            //Costume = ReferenceHelper.GetReferenceObject(this, _reference) as Costume;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("look");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newCostumeRef = new CostumeReference(parent);
            newCostumeRef.Costume = _costume;

            return newCostumeRef;
        }

        public void UpdateReferenceObject()
        {
            Costume = ReferenceHelper.GetReferenceObject(this, _reference) as Costume;
        }

        internal override void LoadReference()
        {
            Costume = ReferenceHelper.GetReferenceObject(this, _reference) as Costume;
        }
    }
}