using System;
using System.Linq;
using System.Xml.Linq;

namespace Catrobat.Core.Converter
{
    public class PathHelper
    {
        public static XElement GetElement(XElement elementRef)
        {
            XElement elementToCopy = elementRef;
            string path = elementRef.Attribute("reference").Value;

            foreach (string part in path.Split('/'))
            {
                if (part == "..")
                    elementToCopy = elementToCopy.Parent;
                else if (part.Contains("["))
                {
                    string newPart = part.Split('[')[0];
                    int pos = Int32.Parse(part.Split('[')[1].Split(']')[0]);
                    elementToCopy = elementToCopy.Elements(newPart).ToArray()[pos - 1];
                }
                else
                    elementToCopy = elementToCopy.Element(part);
            }

            return elementToCopy;
        }

        public static XElement GetSoundListPath(int id)
        {
            string path = "../../../../../soundList/soundInfo";
            if (id > 1)
                path += "[" + id + "]";

            var soundRef = new XElement("Sound");
            var attributeRef = new XAttribute("reference", path);
            soundRef.Add(attributeRef);

            return soundRef;
        }

        public static XElement GetSpritePath(int id)
        {
            string path = "../../../../../../sprite";

            if (id > 1)
                path += "[" + id + "]";

            var spriteRef = new XElement("sprite");
            var attributeRef = new XAttribute("reference", path);
            spriteRef.Add(attributeRef);

            return spriteRef;
        }

        public static XElement GetLoopEndPath(int id)
        {
            string path = "../../loopEndBrick";

            if (id > 1)
                path += "[" + id + "]";

            var loopEndBrick = new XElement("loopEndBrick");
            var attributeRef = new XAttribute("reference", path);
            loopEndBrick.Add(attributeRef);

            return loopEndBrick;
        }
    }
}