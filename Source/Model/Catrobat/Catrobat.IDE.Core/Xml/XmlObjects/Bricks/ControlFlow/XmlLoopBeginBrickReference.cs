using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public class XmlLoopBeginBrickReference : XmlObject
    {
        internal string _reference;

        private XmlLoopBeginBrick _loopBeginBrick;
        public XmlLoopBeginBrick LoopBeginBrick
        {
            get { return _loopBeginBrick; }
            set
            {
                if (_loopBeginBrick == value)
                    return;

                _loopBeginBrick = value;
                RaisePropertyChanged();
            }
        }


        public XmlLoopBeginBrickReference() 
        {
        }


        public XmlLoopBeginBrickReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("loopBeginBrick");
            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(LoopBeginBrick == null)
                LoopBeginBrick = ReferenceHelper.GetReferenceObject(this, _reference) as XmlLoopBeginBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public XmlObject Copy()
        {
            var newLoopBeginBrickRef = new XmlLoopBeginBrickReference
            {
                LoopBeginBrick = _loopBeginBrick
            };

            return newLoopBeginBrickRef;
        }

        public override bool Equals(XmlObject other)
        {
            var otherReference = other as XmlLoopBeginBrickReference;

            if (otherReference == null)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}