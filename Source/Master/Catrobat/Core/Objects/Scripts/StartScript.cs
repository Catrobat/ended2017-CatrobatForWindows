using System.Xml.Linq;
using Catrobat.Core.Objects.Bricks;

namespace Catrobat.Core.Objects.Scripts
{
    public class StartScript : Script
    {
        public StartScript() {}

        public StartScript(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("startScript");

            CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newStartScript = new StartScript();
            if (_bricks != null)
            {
                newStartScript._bricks = _bricks.Copy() as BrickList;
            }

            return newStartScript;
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}