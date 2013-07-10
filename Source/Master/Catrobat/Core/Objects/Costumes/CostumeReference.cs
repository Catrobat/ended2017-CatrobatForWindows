using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Costumes
{
    public class CostumeReference : DataObject
    {
        private readonly Sprite _sprite;

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

        private string _reference;
        public string Reference
        {
            get { return _reference; }
            set
            {
                if (_reference == value)
                {
                    return;
                }

                _reference = value;
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
            _costume = XPathHelper.GetElement(_reference, _sprite) as Costume;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("look");

            xRoot.Add(new XAttribute("reference", XPathHelper.GetReference(_costume, _sprite)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newCostumeRef = new CostumeReference(parent);
            newCostumeRef._reference = _reference;
            newCostumeRef._costume = XPathHelper.GetElement(_reference, parent) as Costume;

            return newCostumeRef;
        }
    }
}