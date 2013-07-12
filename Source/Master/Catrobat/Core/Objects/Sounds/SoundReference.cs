using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Sounds
{
    public class SoundReference : DataObject
    {
        private readonly Sprite _sprite;
        private string _reference;

        private Sound _sound;
        public Sound Sound
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

        public SoundReference(Sprite parent)
        {
            _sprite = parent;
        }

        public SoundReference(XElement xElement, Sprite parent)
        {
            _sprite = parent;
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            Sound = ReferenceHelper.GetReferenceObject(this, _reference) as Sound;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("sound");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newSoundInfoRef = new SoundReference(parent);
            newSoundInfoRef.Sound = _sound;

            return newSoundInfoRef;
        }

        public void UpdateReferenceObject()
        {
            Sound = ReferenceHelper.GetReferenceObject(this, _reference) as Sound;
        }
    }
}