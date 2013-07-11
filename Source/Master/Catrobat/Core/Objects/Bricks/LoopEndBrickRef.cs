using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopEndBrickRef : DataObject
    {
        private readonly Sprite _sprite;

        private LoopEndBrick _loopEndBrick;
        public LoopEndBrick LoopEndBrick
        {
            get { return _loopEndBrick; }
            set
            {
                if (_loopEndBrick == value)
                {
                    return;
                }

                _loopEndBrick = value;
                RaisePropertyChanged();
            }
        }

        protected string _reference;
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


        public LoopEndBrickRef(Sprite parent)
        {
            _sprite = parent;
        }

        public LoopEndBrickRef(XElement xElement, Sprite parent)
        {
            _sprite = parent;
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _reference = xRoot.Attribute("reference").Value;
            _loopEndBrick = XPathHelper.GetElement(_reference, _sprite) as LoopEndBrick;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopEndBrick");
            xRoot.Add(new XAttribute("reference", XPathHelper.GetReference(_loopEndBrick, _sprite)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newLoopEndBrickRef = new LoopEndBrickRef(parent);
            newLoopEndBrickRef._reference = _reference;
            newLoopEndBrickRef._loopEndBrick = XPathHelper.GetElement(_reference, parent) as LoopEndBrick;

            return newLoopEndBrickRef;
        }
    }
}