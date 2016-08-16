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

        #region equals_and_gethashcode
        public override bool Equals(System.Object obj)
        {
            XmlSoundReference s = obj as XmlSoundReference;
            if ((object)s == null)
            {
                return false;
            }

            return this.Equals(s);
        }

        public bool Equals(XmlSoundReference s)
        {
            return this._reference.Equals(s._reference) && this.Sound.Equals(s.Sound);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ _reference.GetHashCode() ^ Sound.GetHashCode();
        }
        #endregion

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute(XmlConstants.Reference).Value;
            //Sound = ReferenceHelper.GetReferenceObject(this, _reference) as Sound;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Sound);

            xRoot.Add(new XAttribute(XmlConstants.Reference, ReferenceHelper.GetReferenceString(this)));
            return xRoot;
        }

        public override void LoadReference()
        {
            if(Sound == null)
                Sound = ReferenceHelper.GetReferenceObject(this, _reference) as XmlSound;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }
    }
}
