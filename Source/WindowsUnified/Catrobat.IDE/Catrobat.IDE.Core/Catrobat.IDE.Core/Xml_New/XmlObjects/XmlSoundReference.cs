using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public class XmlSoundReference : XmlObjectNode
    {
        private string _reference;

        public XmlSound Sound { get; set; }

        public XmlSoundReference()
        {
        }

        public XmlSoundReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            //Sound = ReferenceHelper.GetReferenceObject(this, _reference) as Sound;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("sound");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(Sound == null)
                Sound = ReferenceHelper.GetReferenceObject(this, _reference) as XmlSound;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}