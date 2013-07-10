using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Sounds
{
    public class SoundList : DataObject
    {
        private Sprite _sprite;

        public SoundList(Sprite parent)
        {
            Sounds = new ObservableCollection<Sound>();
            _sprite = parent;
        }

        public SoundList(XElement xElement, Sprite parent)
        {
            _sprite = parent;
            LoadFromXML(xElement);
        }

        public ObservableCollection<Sound> Sounds { get; set; }

        public Sprite Sprite
        {
            get { return _sprite; }
            set
            {
                if (_sprite == value)
                {
                    return;
                }

                _sprite = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            Sounds = new ObservableCollection<Sound>();
            foreach (XElement element in xRoot.Elements())
            {
                Sounds.Add(new Sound(element, _sprite));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("soundList");

            foreach (Sound sound in Sounds)
            {
                xRoot.Add(sound.CreateXML());
            }

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newSoundList = new SoundList(parent);
            foreach (Sound info in Sounds)
            {
                newSoundList.Sounds.Add(info.Copy(parent) as Sound);
            }

            return newSoundList;
        }

        public void Delete()
        {
            foreach (Sound sound in Sounds)
            {
                sound.Delete();
            }
        }
    }
}