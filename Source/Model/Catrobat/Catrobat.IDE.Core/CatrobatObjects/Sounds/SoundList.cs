using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Sounds
{
    public class SoundList : DataObject
    {
        public ObservableCollection<Sound> Sounds { get; set; }


        public SoundList()
        {
            Sounds = new ObservableCollection<Sound>();
        }

        public SoundList(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            Sounds = new ObservableCollection<Sound>();
            foreach (XElement element in xRoot.Elements())
            {
                Sounds.Add(new Sound(element));
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

        public async Task<DataObject> Copy()
        {
            var newSoundList = new SoundList();
            foreach (Sound info in Sounds)
                newSoundList.Sounds.Add(await info.Copy() as Sound);

            return newSoundList;
        }

        public async Task Delete()
        {
            foreach (Sound sound in Sounds)
                await sound.Delete();
        }

        public override bool Equals(DataObject other)
        {
            var otherSoundList = other as SoundList;

            if (otherSoundList == null)
                return false;

            var count = Sounds.Count;
            var otherCount = otherSoundList.Sounds.Count;

            if (count != otherCount)
                return false;

            for (int i = 0; i < count; i++)
                if (!Sounds[i].Equals(otherSoundList.Sounds[i]))
                    return false;

            return true;
        }
    }
}