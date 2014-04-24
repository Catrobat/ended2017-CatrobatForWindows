using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects.Sounds;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
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
                RaisePropertyChanged(() => Sound);
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
                    _soundReference = new SoundReference();

                if (_soundReference.Sound == value)
                    return;

                _soundReference.Sound = value;

                if (value == null)
                    _soundReference = null;

                RaisePropertyChanged();
            }
        }


        public PlaySoundBrick() { }

        public PlaySoundBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("sound") != null)
            {
                _soundReference = new SoundReference(xRoot.Element("sound"));
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

        internal override void LoadReference(XmlFormulaTreeConverter converter)
        {
            if (_soundReference != null)
                _soundReference.LoadReference();
        }

        public override DataObject Copy()
        {
            var newBrick = new PlaySoundBrick();

            if (_soundReference != null)
                newBrick._soundReference = _soundReference.Copy() as SoundReference;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as PlaySoundBrick;

            if (otherBrick == null)
                return false;

            return SoundReference.Equals(otherBrick.SoundReference);
        }
    }
}