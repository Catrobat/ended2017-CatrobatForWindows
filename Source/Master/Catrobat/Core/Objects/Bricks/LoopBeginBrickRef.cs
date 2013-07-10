using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopBeginBrickRef : DataObject
    {
        private readonly Sprite _sprite;

        protected string _classField;

        private LoopBeginBrick _loopBeginBrick;
        protected string _reference;

        public LoopBeginBrickRef(Sprite parent)
        {
            _sprite = parent;
        }

        public LoopBeginBrickRef(XElement xElement, Sprite parent)
        {
            _sprite = parent;
            LoadFromXML(xElement);
        }

        public string Class
        {
            get { return _classField; }
            set
            {
                if (_classField == value)
                {
                    return;
                }

                _classField = value;
                RaisePropertyChanged();
            }
        }

        public string Reference
        {
            get { return _reference; }
            set
            {
                if (_reference == value)
                {
                    return;
                }

                _reference = value;
                RaisePropertyChanged();
            }
        }

        public LoopBeginBrick LoopBeginBrick
        {
            get { return _loopBeginBrick; }
            set
            {
                if (_loopBeginBrick == value)
                {
                    return;
                }

                _loopBeginBrick = value;
                RaisePropertyChanged();
            }
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _classField = xRoot.Attribute("class").Value;
            _reference = xRoot.Attribute("reference").Value;
            _loopBeginBrick = XPathHelper.GetElement(_reference, _sprite) as LoopBeginBrick;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopBeginBrick");
            xRoot.Add(new XAttribute("class", _classField));
            xRoot.Add(new XAttribute("reference", XPathHelper.GetReference(_loopBeginBrick, _sprite)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newLoopBeginBrickRef = new LoopBeginBrickRef(parent);
            newLoopBeginBrickRef._classField = _classField;
            newLoopBeginBrickRef._reference = _reference;
            newLoopBeginBrickRef._loopBeginBrick = XPathHelper.GetElement(_reference, _sprite) as LoopBeginBrick;

            return newLoopBeginBrickRef;
        }
    }
}