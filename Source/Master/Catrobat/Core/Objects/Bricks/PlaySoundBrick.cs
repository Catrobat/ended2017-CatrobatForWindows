using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects.Sounds;

namespace Catrobat.Core.Objects.Bricks
{
    public class PlaySoundBrick : Brick
    {
        private SoundReference soundInfoReference;

        public PlaySoundBrick()
        {
        }

        public PlaySoundBrick(Sprite parent) : base(parent)
        {
        }

        public PlaySoundBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        internal SoundReference SoundInfoReference
        {
            get { return soundInfoReference; }
            set
            {
                if (soundInfoReference == value)
                    return;

                soundInfoReference = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SoundInfoReference"));
            }
        }

        public Sound Sound
        {
            get
            {
                if (soundInfoReference == null)
                    return null;

                return soundInfoReference.Sound;
            }
            set
            {
                if (soundInfoReference == null)
                {
                    soundInfoReference = new SoundReference(sprite);
                    soundInfoReference.Reference = XPathHelper.getReference(value, sprite);
                }

                if (soundInfoReference.Sound == value)
                    return;

                soundInfoReference.Sound = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sound"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("Sound") != null)
                soundInfoReference = new SoundReference(xRoot.Element("Sound"), sprite);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("playSoundBrick");

            if (soundInfoReference != null)
                xRoot.Add(soundInfoReference.CreateXML());

            ////CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new PlaySoundBrick(parent);
            if (soundInfoReference != null)
                newBrick.soundInfoReference = soundInfoReference.Copy(parent) as SoundReference;

            return newBrick;
        }
    }
}