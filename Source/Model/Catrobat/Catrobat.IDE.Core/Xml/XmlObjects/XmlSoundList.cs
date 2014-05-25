using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlSoundList : XmlObject
    {
        public ObservableCollection<XmlSound> Sounds { get; set; }


        public XmlSoundList()
        {
            Sounds = new ObservableCollection<XmlSound>();
        }

        public XmlSoundList(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            Sounds = new ObservableCollection<XmlSound>();
            foreach (XElement element in xRoot.Elements())
            {
                Sounds.Add(new XmlSound(element));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("soundList");

            foreach (XmlSound sound in Sounds)
            {
                xRoot.Add(sound.CreateXml());
            }

            return xRoot;
        }

        public async Task<XmlObject> Copy()
        {
            var newSoundList = new XmlSoundList();
            foreach (XmlSound info in Sounds)
                newSoundList.Sounds.Add(await info.Copy() as XmlSound);

            return newSoundList;
        }

        public override bool Equals(XmlObject other)
        {
            var otherSoundList = other as XmlSoundList;

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