using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlSoundReference : XmlObject
    {
        internal string _reference;

        private XmlSound _sound;
        public XmlSound Sound
        {
            get { return _sound; }
            set
            {
                if (_sound == value)
                {
                    return;
                }

                _sound = value;
                RaisePropertyChanged();
            }
        }

        public XmlSoundReference()
        {
        }

        public XmlSoundReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            //Sound = ReferenceHelper.GetReferenceObject(this, _reference) as Sound;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("sound");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(Sound == null)
                Sound = ReferenceHelper.GetReferenceObject(this, _reference) as XmlSound;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public XmlObject Copy()
        {
            var newSoundInfoRef = new XmlSoundReference();
            newSoundInfoRef.Sound = _sound;

            return newSoundInfoRef;
        }

        public override bool Equals(XmlObject other)
        {
            var otherReference = other as XmlSoundReference;

            if (otherReference == null)
                return false;

            if (Sound.Name != otherReference.Sound.Name)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}