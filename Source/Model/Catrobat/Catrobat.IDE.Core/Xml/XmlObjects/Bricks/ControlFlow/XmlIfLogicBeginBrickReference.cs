using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public class XmlIfLogicBeginBrickReference : XmlObject
    {
        internal string _reference;

        private XmlIfLogicBeginBrick _ifLogicBeginBrick;
        public XmlIfLogicBeginBrick IfLogicBeginBrick
        {
            get { return _ifLogicBeginBrick; }
            set
            {
                if (_ifLogicBeginBrick == value)
                    return;

                _ifLogicBeginBrick = value;
                RaisePropertyChanged();
            }
        }


        public XmlIfLogicBeginBrickReference()
        {
        }

        public XmlIfLogicBeginBrickReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("ifBeginBrick");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(IfLogicBeginBrick == null)
                IfLogicBeginBrick = ReferenceHelper.GetReferenceObject(this, _reference) as XmlIfLogicBeginBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public XmlObject Copy()
        {
            var newIfLogicBeginBrickRef = new XmlIfLogicBeginBrickReference();
            newIfLogicBeginBrickRef.IfLogicBeginBrick = _ifLogicBeginBrick;

            return newIfLogicBeginBrickRef;
        }

        public override bool Equals(XmlObject other)
        {
            var otherReference = other as XmlIfLogicBeginBrickReference;

            if (otherReference == null)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}