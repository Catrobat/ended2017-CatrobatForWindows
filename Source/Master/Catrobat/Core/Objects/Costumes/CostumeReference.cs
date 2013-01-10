using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Costumes
{
    public class CostumeReference : DataObject
    {
        private readonly Sprite sprite;

        private Costumes.Costume costume;
        private string reference;

        public CostumeReference(Sprite parent)
        {
            sprite = parent;
        }

        public CostumeReference(XElement xElement, Sprite parent)
        {
            sprite = parent;
            LoadFromXML(xElement);
        }

        public string Reference
        {
            get { return reference; }
            set
            {
                if (reference == value)
                    return;

                reference = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Reference"));
            }
        }

        public Costumes.Costume Costume
        {
            get { return costume; }
            set
            {
                if (costume == value)
                    return;

                costume = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Costume"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            reference = xRoot.Attribute("reference").Value;
            costume = XPathHelper.getElement(reference, sprite) as Costumes.Costume;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("costumeData");

            xRoot.Add(new XAttribute("reference", XPathHelper.getReference(costume, sprite)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newCostumeRef = new CostumeReference(parent);
            newCostumeRef.reference = reference;
            newCostumeRef.costume = XPathHelper.getElement(reference, parent) as Costumes.Costume;

            return newCostumeRef;
        }
    }
}