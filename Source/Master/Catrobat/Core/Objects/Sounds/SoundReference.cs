using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Sounds
{
    public class SoundReference : DataObject
    {
        private readonly Sprite sprite;

        private string reference;

        private Sound sound;

        public SoundReference(Sprite parent)
        {
            sprite = parent;
        }

        public SoundReference(XElement xElement, Sprite parent)
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

        public Sound Sound
        {
            get { return sound; }
            set
            {
                if (sound == value)
                    return;

                sound = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sound"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            reference = xRoot.Attribute("reference").Value;
            Sound = XPathHelper.getElement(reference, sprite) as Sound;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("Sound");

            xRoot.Add(new XAttribute("reference", XPathHelper.getReference(Sound, sprite)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newSoundInfoRef = new SoundReference(parent);
            newSoundInfoRef.reference = reference;
            newSoundInfoRef.Sound = XPathHelper.getElement(reference, parent) as Sound;

            return newSoundInfoRef;
        }
    }
}