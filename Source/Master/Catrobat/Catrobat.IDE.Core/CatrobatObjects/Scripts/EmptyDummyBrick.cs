using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Scripts
{
    public class EmptyDummyBrick : Script
    {

        internal override void LoadFromXML(XElement xRoot)
        {
            
        }

        internal override XElement CreateXML()
        {
            return null;
        }

        public override DataObject Copy()
        {
            return new EmptyDummyBrick();
        }

        public override bool Equals(DataObject other)
        {
            return other is EmptyDummyBrick;
        }
    }
}