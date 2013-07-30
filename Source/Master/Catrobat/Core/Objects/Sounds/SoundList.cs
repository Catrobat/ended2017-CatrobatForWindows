using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Sounds
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

        public DataObject Copy()
        {
            var newSoundList = new SoundList();
            foreach (Sound info in Sounds)
                newSoundList.Sounds.Add(info.Copy() as Sound);

            return newSoundList;
        }

        public void Delete()
        {
            foreach (Sound sound in Sounds)
                sound.Delete();
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}