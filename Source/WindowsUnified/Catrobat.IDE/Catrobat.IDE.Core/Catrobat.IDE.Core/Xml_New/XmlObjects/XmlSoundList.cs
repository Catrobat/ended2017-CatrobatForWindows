using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlSoundList : XmlObjectNode
    {
        public List<XmlSound> Sounds { get; set; }

        public XmlSoundList()
        {
            Sounds = new List<XmlSound>();
        }

        public XmlSoundList(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            Sounds = new List<XmlSound>();
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
    }
}