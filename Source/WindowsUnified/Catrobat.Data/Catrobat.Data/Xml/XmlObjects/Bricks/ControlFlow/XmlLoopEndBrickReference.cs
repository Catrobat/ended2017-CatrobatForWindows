using System.Xml.Linq;
using Catrobat.Data.Utilities.Helpers;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow
{
    public class XmlLoopEndBrickReference : XmlObject
    {
        private string _reference;

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

        public XmlLoopEndBrick LoopEndBrick { get; set; }

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
    }
}