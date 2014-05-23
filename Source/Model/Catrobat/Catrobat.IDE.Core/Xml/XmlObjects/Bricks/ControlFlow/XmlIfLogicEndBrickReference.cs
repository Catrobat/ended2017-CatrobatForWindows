using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public class XmlIfLogicEndBrickReference : XmlObject
    {
        internal string _reference;

        private XmlIfLogicEndBrick _ifLogicEndBrick;
        public XmlIfLogicEndBrick IfLogicEndBrick
        {
            get { return _ifLogicEndBrick; }
            set
            {
                if (_ifLogicEndBrick == value)
                    return;

                _ifLogicEndBrick = value;
                RaisePropertyChanged();
            }
        }


        public XmlIfLogicEndBrickReference()
        {
        }

        public XmlIfLogicEndBrickReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("ifEndBrick");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(IfLogicEndBrick == null)
                IfLogicEndBrick = ReferenceHelper.GetReferenceObject(this, _reference) as XmlIfLogicEndBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public XmlObject Copy()
        {
            var newIfLogicEndBrickRef = new XmlIfLogicEndBrickReference();
            newIfLogicEndBrickRef.IfLogicEndBrick = _ifLogicEndBrick;

            return newIfLogicEndBrickRef;
        }

        public override bool Equals(XmlObject other)
        {
            var otherReference = other as XmlIfLogicEndBrickReference;

            if (otherReference == null)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}