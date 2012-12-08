using System;
using Catrobat.Core.Objects;

namespace Catrobat.Core.Helpers
{
    public class XPathHelper
    {
        public static DataObject getElement(string reference, Sprite sprite)
        {
            reference = reference.ToLower();

            if (reference.Contains("costume"))
                return getCostume(reference, sprite);
            else if (reference.Contains("sound"))
                return getSoundInfo(reference, sprite);
            else if (reference.Contains("forever") || reference.Contains("repeat"))
                return getLoopBeginBrick(reference);
            else if (reference.Contains("loopend"))
                return getLoopEndBrick(reference);
            else
                return getSprite(reference, sprite);
        }

        public static string getReference(DataObject dataObject, Sprite spriteContainingDataObject)
        {
            string reference = "";
            int pos = 0;
            bool found = false;

            if (dataObject is SoundInfo)
            {
                reference = "../../../../../soundList/soundInfo";
                foreach (SoundInfo soundInfo in spriteContainingDataObject.Sounds.Sounds)
                {
                    pos++;
                    if (soundInfo == dataObject)
                    {
                        found = true;
                        break;
                    }
                }
                if (pos > 1)
                    reference += "[" + pos + "]";
            }
            else if (dataObject is CostumeData)
            {
                reference = "../../../../../costumeDataList/costumeData";
                foreach (CostumeData costume in spriteContainingDataObject.Costumes.Costumes)
                {
                    pos++;
                    if (costume == dataObject)
                    {
                        found = true;
                        break;
                    }
                }
                if (pos > 1)
                    reference += "[" + pos + "]";
            }
            else if (dataObject is Sprite)
            {
                reference = "../../../../../../sprite";
                foreach (Sprite pointedSprite in spriteContainingDataObject.Project.SpriteList.Sprites)
                {
                    pos++;
                    if (pointedSprite == dataObject)
                    {
                        found = true;
                        break;
                    }
                }
                if (pos > 1)
                    reference += "[" + pos + "]";
            }
            else if (dataObject is ForeverBrick)
            {
                reference = "../../foreverBrick";
                foreach (Script script in spriteContainingDataObject.Scripts.Scripts)
                {
                    pos = 0;
                    foreach (DataObject brick in script.Bricks.Bricks)
                    {
                        if (brick is ForeverBrick)
                        {
                            pos++;
                            if (brick == dataObject)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found)
                        break;
                }
                if (pos > 1)
                    reference += "[" + pos + "]";
            }
            else if (dataObject is RepeatBrick)
            {
                reference = "../../repeatBrick";
                foreach (Script script in spriteContainingDataObject.Scripts.Scripts)
                {
                    pos = 0;
                    foreach (DataObject brick in script.Bricks.Bricks)
                    {
                        if (brick is RepeatBrick)
                        {
                            pos++;
                            if (brick == dataObject)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found)
                        break;
                }
                if (pos > 1)
                    reference += "[" + pos + "]";
            }
            else if (dataObject is LoopEndBrick)
            {
                reference = "../../loopEndBrick";
                foreach (Script script in spriteContainingDataObject.Scripts.Scripts)
                {
                    pos = 0;
                    foreach (DataObject brick in script.Bricks.Bricks)
                    {
                        if (brick is LoopEndBrick)
                        {
                            pos++;
                            if (brick == dataObject)
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found)
                        break;
                }
                if (pos > 1)
                    reference += "[" + pos + "]";
            }

            if (!found)
                return "";

            return reference;
        }

        private static CostumeData getCostume(string xPath, Sprite sprite)
        {
            int pos = 0;

            if (xPath.Contains("["))
            {
                xPath = xPath.Split('[')[1];
                xPath = xPath.Split(']')[0];
                pos = Int32.Parse(xPath) - 1;
            }

            return sprite.Costumes.Costumes[pos];
        }

        private static SoundInfo getSoundInfo(string xPath, Sprite sprite)
        {
            int pos = 0;

            if (xPath.Contains("["))
            {
                xPath = xPath.Split('[')[1];
                xPath = xPath.Split(']')[0];
                pos = Int32.Parse(xPath) - 1;
            }

            return sprite.Sounds.Sounds[pos];
        }

        private static Sprite getSprite(string xPath, Sprite sprite)
        {
            int pos = 0;

            if (xPath.Contains("["))
            {
                xPath = xPath.Split('[')[1];
                xPath = xPath.Split(']')[0];
                pos = Int32.Parse(xPath) - 1;
            }

            return sprite.Project.SpriteList.Sprites[pos];
        }

        private static LoopBeginBrick getLoopBeginBrick(string xPath)
        {
            int pos = 0;
            int id = 0;
            if (xPath.EndsWith("]"))
            {
                int start = xPath.LastIndexOf('[') + 1;
                pos = Int32.Parse(xPath.Substring(start, xPath.Length - start - 1)) - 1;
            }

            if (xPath.Contains("forever"))
            {
                foreach (Brick brick in ReadHelper.currentBrickList.Bricks)
                    if (brick is ForeverBrick)
                    {
                        if (id == pos)
                            return brick as LoopBeginBrick;
                        id++;
                    }
            }
            else if (xPath.Contains("repeat"))
            {
                foreach (Brick brick in ReadHelper.currentBrickList.Bricks)
                    if (brick is RepeatBrick)
                    {
                        if (id == pos)
                            return brick as LoopBeginBrick;
                        id++;
                    }
            }

            return null;
        }

        private static LoopEndBrick getLoopEndBrick(string xPath)
        {
            int pos = 0;
            int id = 0;
            if (xPath.EndsWith("]"))
            {
                int start = xPath.LastIndexOf('[') + 1;
                pos = Int32.Parse(xPath.Substring(start, xPath.Length - start - 1)) - 1;
            }

            foreach (Brick brick in ReadHelper.currentBrickList.Bricks)
                if (brick is LoopEndBrick)
                {
                    if (id == pos)
                        return brick as LoopEndBrick;
                    id++;
                }

            return null;
        }
    }
}