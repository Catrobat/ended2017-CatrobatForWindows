using System.Xml.Linq;

namespace Catrobat.Core.Converter
{
    public static class Converter
    {
        public static void Convert(XDocument doc)
        {
            if (doc == null)
            {
                return;
            }

            if (doc.Root.Element("applicationXmlVersion") != null)
            {
                return;
            }

            EditXML.RemoveSpriteReferences(doc);
            EditXML.HandleProjectElements(doc);
            EditXML.HandlePointToBrick(doc);
            EditXML.HandleSounds(doc);
            EditXML.HandleLoops(doc);
            EditXML.RemoveNameSpaces(doc);
            EditXML.ChangeReferences(doc);
        }
    }
}