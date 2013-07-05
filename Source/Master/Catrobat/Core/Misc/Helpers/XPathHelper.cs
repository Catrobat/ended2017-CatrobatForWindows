using System;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Sounds;

namespace Catrobat.Core.Misc.Helpers
{
    public class XPathHelper
    {
        public static DataObject getElement(string reference, Sprite sprite)
        {
            reference = reference.ToLower();

            if (reference.Contains("costume"))
            {
                return getCostume(reference, sprite);
            }
            else if (reference.Contains("sound"))
            {
                return getSoundInfo(reference, sprite);
            }
            else if (reference.Contains("forever") || reference.Contains("repeat"))
            {
                return getLoopBeginBrick(reference);
            }
            else if (reference.Contains("loopend"))
            {
                return getLoopEndBrick(reference);
            }
            else
            {
                return getSprite(reference, sprite);
            }
        }

        public static string getReference(DataObject dataObject, Sprite spriteContainingDataObject)
        {
            var reference = "";
            var pos = 0;
            var found = false;

            if (dataObject is Sound)
            {
                reference = "../../../../../soundList/soundInfo";
                foreach (Sound Sound in spriteContainingDataObject.Sounds.Sounds)
                {
                    pos++;
                    if (Sound == dataObject)
                    {
                        found = true;
                        break;
                    }
                }
                if (pos > 1)
                {
                    reference += "[" + pos + "]";
                }
            }
            else if (dataObject is Costume)
            {
                reference = "../../../../../costumeDataList/costumeData";
                foreach (Costume costume in spriteContainingDataObject.Costumes.Costumes)
                {
                    pos++;
                    if (costume == dataObject)
                    {
                        found = true;
                        break;
                    }
                }
                if (pos > 1)
                {
                    reference += "[" + pos + "]";
                }
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
                {
                    reference += "[" + pos + "]";
                }
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
                    {
                        break;
                    }
                }
                if (pos > 1)
                {
                    reference += "[" + pos + "]";
                }
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
                    {
                        break;
                    }
                }
                if (pos > 1)
                {
                    reference += "[" + pos + "]";
                }
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
                    {
                        break;
                    }
                }
                if (pos > 1)
                {
                    reference += "[" + pos + "]";
                }
            }

            if (!found)
            {
                return "";
            }

            return reference;
        }

        private static Costume getCostume(string xPath, Sprite sprite)
        {
            var pos = 0;

            if (xPath.Contains("["))
            {
                xPath = xPath.Split('[')[1];
                xPath = xPath.Split(']')[0];
                pos = Int32.Parse(xPath) - 1;
            }

            return sprite.Costumes.Costumes[pos];
        }

        private static Sound getSoundInfo(string xPath, Sprite sprite)
        {
            var pos = 0;

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
            var pos = 0;

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
            var pos = 0;
            var id = 0;
            if (xPath.EndsWith("]"))
            {
                var start = xPath.LastIndexOf('[') + 1;
                pos = Int32.Parse(xPath.Substring(start, xPath.Length - start - 1)) - 1;
            }

            if (xPath.Contains("forever"))
            {
                foreach (Brick brick in ReadHelper.CurrentBrickList.Bricks)
                {
                    if (brick is ForeverBrick)
                    {
                        if (id == pos)
                        {
                            return brick as LoopBeginBrick;
                        }
                        id++;
                    }
                }
            }
            else if (xPath.Contains("repeat"))
            {
                foreach (Brick brick in ReadHelper.CurrentBrickList.Bricks)
                {
                    if (brick is RepeatBrick)
                    {
                        if (id == pos)
                        {
                            return brick as LoopBeginBrick;
                        }
                        id++;
                    }
                }
            }

            return null;
        }

        private static LoopEndBrick getLoopEndBrick(string xPath)
        {
            var pos = 0;
            var id = 0;
            if (xPath.EndsWith("]"))
            {
                var start = xPath.LastIndexOf('[') + 1;
                pos = Int32.Parse(xPath.Substring(start, xPath.Length - start - 1)) - 1;
            }

            foreach (Brick brick in ReadHelper.CurrentBrickList.Bricks)
            {
                if (brick is LoopEndBrick)
                {
                    if (id == pos)
                    {
                        return brick as LoopEndBrick;
                    }
                    id++;
                }
            }

            return null;
        }
    }
}