using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Helpers;
using Catrobat.Core.Objects.Sounds;

namespace Catrobat.Core.Objects
{
    public class PlaySoundBrick : Brick
    {
        private SoundInfoReference soundInfoReference;

        public PlaySoundBrick()
        {
        }

        public PlaySoundBrick(Sprite parent) : base(parent)
        {
        }

        public PlaySoundBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        internal SoundInfoReference SoundInfoReference
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

        public SoundInfo SoundInfo
        {
            get
            {
                if (soundInfoReference == null)
                    return null;

                return soundInfoReference.SoundInfo;
            }
            set
            {
                if (soundInfoReference == null)
                {
                    soundInfoReference = new SoundInfoReference(sprite);
                    soundInfoReference.Reference = XPathHelper.getReference(value, sprite);
                }

                if (soundInfoReference.SoundInfo == value)
                    return;

                soundInfoReference.SoundInfo = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SoundInfo"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("soundInfo") != null)
                soundInfoReference = new SoundInfoReference(xRoot.Element("soundInfo"), sprite);
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
                newBrick.soundInfoReference = soundInfoReference.Copy(parent) as SoundInfoReference;

            return newBrick;
        }
    }
}