using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public class XmlIfLogicElseBrickReference : XmlObject
    {
        internal string _reference;

        private XmlIfLogicElseBrick _ifLogicElseBrick;
        public XmlIfLogicElseBrick IfLogicElseBrick
        {
            get { return _ifLogicElseBrick; }
            set
            {
                if (_ifLogicElseBrick == value)
                    return;

                _ifLogicElseBrick = value;
                RaisePropertyChanged();
            }
        }


        public XmlIfLogicElseBrickReference()
        {
        }

        public XmlIfLogicElseBrickReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("ifElseBrick");

            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(IfLogicElseBrick == null)
                IfLogicElseBrick = ReferenceHelper.GetReferenceObject(this, _reference) as XmlIfLogicElseBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public XmlObject Copy()
        {
            var newIfLogicElseBrickRef = new XmlIfLogicElseBrickReference();
            newIfLogicElseBrickRef.IfLogicElseBrick = _ifLogicElseBrick;

            return newIfLogicElseBrickRef;
        }

        public override bool Equals(XmlObject other)
        {
            var otherReference = other as XmlIfLogicElseBrickReference;

            if (otherReference == null)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}