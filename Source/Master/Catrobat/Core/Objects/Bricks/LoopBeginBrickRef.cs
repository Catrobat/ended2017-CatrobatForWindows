using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopBeginBrickRef : DataObject
    {
        private readonly Sprite _sprite;
        private string _reference;

        protected string _classField;
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

        private LoopBeginBrick _loopBeginBrick;
        public LoopBeginBrick LoopBeginBrick
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


        public LoopBeginBrickRef(Sprite parent)
        {
            _sprite = parent;
        }

        public LoopBeginBrickRef(XElement xElement, Sprite parent)
        {
            _sprite = parent;
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            _classField = xRoot.Attribute("class").Value;
            _reference = xRoot.Attribute("reference").Value;
            
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopBeginBrick");
            xRoot.Add(new XAttribute("class", _classField));
            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        internal override void LoadReference()
        {
            LoopBeginBrick = ReferenceHelper.GetReferenceObject(this, _reference) as LoopBeginBrick;
        }

        public DataObject Copy(Sprite parent)
        {
            var newLoopBeginBrickRef = new LoopBeginBrickRef(parent);
            newLoopBeginBrickRef._classField = _classField;
            newLoopBeginBrickRef._loopBeginBrick = _loopBeginBrick;

            return newLoopBeginBrickRef;
        }
    }
}