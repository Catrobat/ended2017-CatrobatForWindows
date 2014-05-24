using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    public class XmlLoopEndBrickReference : XmlObject
    {
        internal string _reference;

        //protected string _classField;
        //public string Class
        //{
        //    get { return _classField; }
        //    set
        //    {
        //        if (_classField == value)
        //        {
        //            return;
        //        }

        //        _classField = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private XmlLoopEndBrick _loopEndBrick;
        public XmlLoopEndBrick LoopEndBrick
        {
            get { return _loopEndBrick; }
            set
            {
                if (_loopEndBrick == value)
                    return;

                _loopEndBrick = value;
                RaisePropertyChanged();
            }
        }


        public XmlLoopEndBrickReference()
        {
        }

        public XmlLoopEndBrickReference(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            //_classField = xRoot.Attribute("class").Value;
            _reference = xRoot.Attribute("reference").Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("loopEndBrick");
            //xRoot.Add(new XAttribute("class", _classField));
            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            if(LoopEndBrick == null)
                LoopEndBrick = ReferenceHelper.GetReferenceObject(this, _reference) as XmlLoopEndBrick;
            if (string.IsNullOrEmpty(_reference))
                _reference = ReferenceHelper.GetReferenceString(this);
        }

        public XmlObject Copy()
        {
            var newLoopEndBrickRef = new XmlLoopEndBrickReference();
            //newLoopEndBrickRef.Class = _classField;
            newLoopEndBrickRef.LoopEndBrick = _loopEndBrick;

            return newLoopEndBrickRef;
        }

        public override bool Equals(XmlObject other)
        {
            var otherReference = other as XmlLoopEndBrickReference;

            if (otherReference == null)
                return false;

            if (_reference != otherReference._reference)
                return false;

            return true;
        }
    }
}