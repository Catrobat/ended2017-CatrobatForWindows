using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class LoopEndBrickRef : DataObject
    {
        private readonly Sprite _sprite;
        private string _reference;

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
            _loopEndBrick = ReferenceHelper.GetReferenceObject(this, _reference) as LoopEndBrick;
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopEndBrick");
            xRoot.Add(new XAttribute("reference", ReferenceHelper.GetReferenceString(this)));

            return xRoot;
        }

        public DataObject Copy(Sprite parent)
        {
            var newLoopEndBrickRef = new LoopEndBrickRef(parent);
            newLoopEndBrickRef._loopEndBrick = _loopEndBrick;

            return newLoopEndBrickRef;
        }
    }
}