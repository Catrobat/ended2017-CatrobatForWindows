using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class SoundList : DataObject
    {
        private Sprite sprite;

        public SoundList(Sprite parent)
        {
            Sounds = new ObservableCollection<SoundInfo>();
            sprite = parent;
        }

        public SoundList(XElement xElement, Sprite parent)
        {
            sprite = parent;
            LoadFromXML(xElement);
        }

        public ObservableCollection<SoundInfo> Sounds { get; set; }

        public Sprite Sprite
        {
            get { return sprite; }
            set
            {
                if (sprite == value)
                    return;

                sprite = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sprite"));
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            Sounds = new ObservableCollection<SoundInfo>();
            foreach (XElement element in xRoot.Elements())
                Sounds.Add(new SoundInfo(element, sprite));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("soundList");

            foreach (SoundInfo sound in Sounds)
                xRoot.Add(sound.CreateXML());

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newSoundList = new SoundList(parent);
            foreach (SoundInfo info in Sounds)
                newSoundList.Sounds.Add(info.Copy(parent) as SoundInfo);

            return newSoundList;
        }

        public void Delete()
        {
            foreach (SoundInfo sound in Sounds)
                sound.Delete();
        }
    }
}