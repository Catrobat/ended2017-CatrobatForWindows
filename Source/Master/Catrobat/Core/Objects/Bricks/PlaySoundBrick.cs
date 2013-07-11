using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects.Sounds;

namespace Catrobat.Core.Objects.Bricks
{
    public class PlaySoundBrick : Brick
    {
        private SoundReference _soundReference;
        internal SoundReference SoundReference
        {
            get { return _soundReference; }
            set
            {
                if (_soundReference == value)
                {
                    return;
                }

                _soundReference = value;
                RaisePropertyChanged();
            }
        }

        public Sound Sound
        {
            get
            {
                if (_soundReference == null)
                {
                    return null;
                }

                return _soundReference.Sound;
            }
            set
            {
                if (_soundReference == null)
                {
                    _soundReference = new SoundReference(_sprite);
                    _soundReference.Reference = XPathHelper.GetReference(value, _sprite);
                }

                if (_soundReference.Sound == value)
                {
                    return;
                }

                _soundReference.Sound = value;

                if (value == null)
                {
                    _soundReference = null;
                }

                RaisePropertyChanged();
            }
        }


        public PlaySoundBrick() {}

        public PlaySoundBrick(Sprite parent) : base(parent) {}

        public PlaySoundBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("sound") != null)
            {
                _soundReference = new SoundReference(xRoot.Element("sound"), _sprite);
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("playSoundBrick");

            if (_soundReference != null)
            {
                xRoot.Add(_soundReference.CreateXML());
            }

            ////CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new PlaySoundBrick(parent);
            if (_soundReference != null)
            {
                newBrick._soundReference = _soundReference.Copy(parent) as SoundReference;
            }

            return newBrick;
        }

        public void UpdateReference()
        {
            if (_soundReference != null)
            {
                _soundReference.Reference = XPathHelper.GetReference(_soundReference.Sound, _sprite);
            }
        }
    }
}