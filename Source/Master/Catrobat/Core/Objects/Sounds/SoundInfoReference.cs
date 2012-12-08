using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Helpers;

namespace Catrobat.Core.Objects.Sounds
{
    public class SoundInfoReference : DataObject
    {
        private readonly Sprite sprite;

        private string reference;

        private SoundInfo soundInfo;

        public SoundInfoReference(Sprite parent)
        {
            sprite = parent;
        }

        public SoundInfoReference(XElement xElement, Sprite parent)
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

        public SoundInfo SoundInfo
        {
            get { return soundInfo; }
            set
            {
                if (soundInfo == value)
                    return;

                soundInfo = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SoundInfo"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            reference = xRoot.Attribute("reference").Value;
            soundInfo = XPathHelper.getElement(reference, sprite) as SoundInfo;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("soundInfo");

            xRoot.Add(new XAttribute("reference", XPathHelper.getReference(soundInfo, sprite)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newSoundInfoRef = new SoundInfoReference(parent);
            newSoundInfoRef.reference = reference;
            newSoundInfoRef.soundInfo = XPathHelper.getElement(reference, parent) as SoundInfo;

            return newSoundInfoRef;
        }
    }
}