using System;
using System.Collections.ObjectModel;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.Core.Misc.Helpers
{
    public static class XPathHelper
    {
        public static DataObject GetElement(string reference, DataObject parent)
        {
            DataObject retVal = null;
            reference = reference.ToLower();

            if (reference.Contains("costume"))
            {
                retVal = GetCostume(reference, parent as Sprite);
            }
            else if (reference.Contains("sound"))
            {
                retVal = GetSoundInfo(reference, parent as Sprite);
            }
            else if (reference.Contains("userVariable"))
            {
                retVal = GetUserVariable(reference, parent as Sprite);
            }
            else if (reference.Contains("forever") || reference.Contains("repeat"))
            {
                retVal = GetLoopBeginBrick(reference);
            }
            else if (reference.Contains("loopend"))
            {
                retVal = GetLoopEndBrick(reference);
            }
            else
            {
                retVal = GetSprite(reference, parent);
            }

            return retVal;
        }

        public static string GetReference(DataObject dataObject, DataObject parent)
        {
            var reference = "";
            var pos = 0;
            var found = false;

            if (parent is Sprite)
            {
                if (dataObject is Sound)
                {
                    reference = "../../../../../soundList/sound";
                    foreach (Sound sound in (parent as Sprite).Sounds.Sounds)
                    {
                        pos++;
                        if (sound == dataObject)
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
                    reference = "../../../../../lookList/look";
                    foreach (Costume costume in (parent as Sprite).Costumes.Costumes)
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
                    reference = "../../../../../../object";
                    foreach (Sprite pointedSprite in (parent as Sprite).Project.SpriteList.Sprites)
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
                else if (dataObject is UserVariable)
                {
                    reference = "../../../../../../../variables/";
                    var objectVariableList = (parent as Sprite).Project.VariableList.ObjectVariableList;
                    foreach (var entry in objectVariableList.ObjectVariableEntries)
                    {
                        if (entry.SpriteReference.Sprite == parent)
                        {
                            var variables = entry.VariableList;
                            foreach (var variable in variables.UserVariables)
                            {
                                pos++;
                                if (variable == dataObject)
                                {
                                    reference += "objectVariableList/entry/list/userVariable[" + pos + "]";
                                    found = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!found)
                    {
                        pos = 0;
                        var programVariableList = (parent as Sprite).Project.VariableList.ProgramVariableList;
                        foreach (var variable in programVariableList.UserVariables)
                        {
                            pos++;
                            if (variable == dataObject)
                            {
                                reference += "programVariableList/userVariable[" + pos + "]";
                                found = true;
                                break;
                            }
                        }
                    }
                }
                else if (dataObject is ForeverBrick)
                {
                    reference = "../../foreverBrick";
                    foreach (Script script in (parent as Sprite).Scripts.Scripts)
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
                    foreach (Script script in (parent as Sprite).Scripts.Scripts)
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
                    foreach (Script script in (parent as Sprite).Scripts.Scripts)
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
            }
            else
            {
                //TODO: implement if uservariableentry
            }

            return reference;
        }

        private static Costume GetCostume(string xPath, Sprite sprite)
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

        private static Sound GetSoundInfo(string xPath, Sprite sprite)
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

        private static Sprite GetSprite(string xPath, DataObject parent)
        {
            if (parent is Sprite)
            {
                var pos = 0;

                if (xPath.Contains("["))
                {
                    xPath = xPath.Split('[')[1];
                    xPath = xPath.Split(']')[0];
                    pos = Int32.Parse(xPath) - 1;
                }

                return (parent as Sprite).Project.SpriteList.Sprites[pos];
            }
            else
            {
                //TODO: implement if uservariableentry
            }
            return null;
        }

        private static UserVariable GetUserVariable(string xPath, Sprite sprite)
        {
            var variableList = new ObservableCollection<UserVariable>();
            if(xPath.Contains("objectVariableList"))
            {
                var entries = sprite.Project.VariableList.ObjectVariableList.ObjectVariableEntries;
                foreach (var entry in entries)
                {
                    if (entry.SpriteReference.Sprite == sprite)
                        variableList = entry.VariableList.UserVariables;
                }
            }
            else if (xPath.Contains("programVariableList"))
            {
                variableList = sprite.Project.VariableList.ProgramVariableList.UserVariables;
            }

            if (!xPath.Contains("["))
            {
                return variableList[0];
            }
            else
            {
                var pos = 0;
                foreach (var variable in variableList)
                {
                    pos++;
                    if (xPath.Contains(pos.ToString()))
                        return variable;
                }
            }

            return null;
        }

        private static LoopBeginBrick GetLoopBeginBrick(string xPath)
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

        private static LoopEndBrick GetLoopEndBrick(string xPath)
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