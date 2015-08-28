using System;
using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Looks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Variables;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.Utilities.Helpers
{
    public static class ReferenceHelper
    {
        public static string GetReferenceString(XmlObjectNode referenceObject)
        {
            if (referenceObject is XmlLookReference)
                return GetLookReferenceString((referenceObject as XmlLookReference).Look);
            if (referenceObject is XmlSoundReference)
                return GetSoundReferenceString((referenceObject as XmlSoundReference).Sound);
            if (referenceObject is XmlSpriteReference)
                return GetSpriteReferenceString((referenceObject as XmlSpriteReference));
            if (referenceObject is XmlUserVariableReference)
                return GetVariableReferenceString(referenceObject as XmlUserVariableReference);
            if (referenceObject is XmlLoopBeginBrickReference)
            {
                var loopBeginBrickRef = referenceObject as XmlLoopBeginBrickReference;
                if (loopBeginBrickRef.LoopBeginBrick is XmlForeverBrick)
                    return GetForeverBrickReferenceString(loopBeginBrickRef.LoopBeginBrick);
                else
                    return GetRepeatBrickReferenceString(loopBeginBrickRef.LoopBeginBrick);
            }
            if (referenceObject is XmlLoopEndBrickReference)
            {
                var loopEndBrickRef = referenceObject as XmlLoopEndBrickReference;
                if (loopEndBrickRef.LoopEndBrick is XmlForeverLoopEndBrick)
                    return GetForeverLoopEndBrickReferenceString(loopEndBrickRef.LoopEndBrick);
                else
                    return GetRepeatLoopEndBrickReferenceString(loopEndBrickRef.LoopEndBrick);
            }
            if (referenceObject is XmlIfLogicBeginBrickReference)
                return GetIfLogicBeginBrickReferenceString(referenceObject as XmlIfLogicBeginBrickReference);
            if (referenceObject is XmlIfLogicElseBrickReference)
                return GetIfLogicElseBrickReferenceString(referenceObject as XmlIfLogicElseBrickReference);
            if (referenceObject is XmlIfLogicEndBrickReference)
                return GetIfLogicEndBrickReferenceString(referenceObject as XmlIfLogicEndBrickReference);

            return "";
        }

        private static string GetLookReferenceString(XmlLook look)
        {
            var sprite = XmlParserTempProjectHelper.Sprite;

            var count = 0;
            foreach (var tempLook in sprite.Looks.Looks)
            {
                count++;
                if ((tempLook == look) && (count == 1))
                    return "../../../../../lookList/look";
                else if (tempLook == look)
                    return "../../../../../lookList/look[" + count + "]";
            }

            return "";
        }

        private static string GetSoundReferenceString(XmlSound sound)
        {
            var sprite = XmlParserTempProjectHelper.Sprite;

            var count = 0;
            foreach (var tempSound in sprite.Sounds.Sounds)
            {
                count++;
                if ((tempSound == sound) && (count == 1))
                    return "../../../../../soundList/sound";
                else if (tempSound == sound)
                    return "../../../../../soundList/sound[" + count + "]";
            }

            return "";
        }

        private static string GetSpriteReferenceString(XmlSpriteReference xmlSpriteReference)
        {
            var sprite = xmlSpriteReference.Sprite;
            var count = 0;

            foreach (var tempSprite in XmlParserTempProjectHelper.Program.SpriteList.Sprites)
            {
                count++;
                if (tempSprite == sprite)
                    break;
            }

            foreach (var tempSprite in XmlParserTempProjectHelper.Program.SpriteList.Sprites)
                foreach (var script in tempSprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is XmlPointToBrick)
                        {
                            var pointToBrick = brick as XmlPointToBrick;
                            if ((pointToBrick.PointedXmlSpriteReference == xmlSpriteReference) && (count == 1))
                                return "../../../../../../object";
                            else if (pointToBrick.PointedXmlSpriteReference == xmlSpriteReference)
                                return "../../../../../../object[" + count + "]";
                        }
                    }

            foreach (var entry in XmlParserTempProjectHelper.Program.VariableList.ObjectVariableList.ObjectVariableEntries)
            {
                if(( entry.XmlSpriteReference == xmlSpriteReference ) && (count == 1))
                    return "../../../../objectList/object";
                else if (entry.XmlSpriteReference == xmlSpriteReference)
                    return "../../../../objectList/object[" + count + "]";
            }

            return "";
        }

        private static string GetVariableReferenceString(XmlUserVariableReference xmlUserVariableReference)
        {
            var userVariable = xmlUserVariableReference.UserVariable;
            var entryCount = 0;
            foreach (var entry in XmlParserTempProjectHelper.Program.VariableList.ObjectVariableList.ObjectVariableEntries)
            {
                entryCount++;
                var userVariableCount = 0;
                foreach (var tempUserVariable in entry.VariableList.UserVariables)
                {
                    userVariableCount++;
                    if(( tempUserVariable == userVariable  ) && (entryCount == 1) && (userVariableCount == 1))
                        return "../../../../../../../variables/objectVariableList/entry/list/userVariable";

                    else if ((tempUserVariable == userVariable) && (entryCount == 1))
                            return "../../../../../../../variables/objectVariableList/entry/list/userVariable[" + userVariableCount + "]";

                    else if ((tempUserVariable == userVariable) && (userVariableCount == 1))
                            return "../../../../../../../variables/objectVariableList/entry[" + entryCount +
                                    "]/list/userVariable";

                    else if (tempUserVariable == userVariable)
                            return "../../../../../../../variables/objectVariableList/entry[" + entryCount +
                                    "]/list/userVariable[" + userVariableCount + "]";
                }
            }

            var count = 0;
            foreach (var tempUserVariable in XmlParserTempProjectHelper.Program.VariableList.ProgramVariableList.UserVariables)
            {
                count++;
                if ((tempUserVariable == userVariable) && (count == 1))
                    return "../../../../../../../variables/programVariableList/userVariable";
                else if (tempUserVariable == userVariable)
                    return "../../../../../../../variables/programVariableList/userVariable[" + count + "]";
            }

            return "";
        }

        private static string GetForeverBrickReferenceString(XmlLoopBeginBrick loopBeginBrick)
        {
            var sprite = XmlParserTempProjectHelper.Sprite;

            var script = XmlParserTempProjectHelper.Script;
            //TODO could this be dead code as since v93 there are no references like this?
                var count = 0;
                foreach (var brick in script.Bricks.Bricks)
                    if (brick is XmlForeverBrick)
                    {
                        count++;
                        if ((brick == loopBeginBrick) && (count == 1))
                            return "../../foreverBrick";
                        else if (brick == loopBeginBrick)
                            return "../../foreverBrick[" + count + "]";
                    }

            return "";
        }

        private static string GetRepeatBrickReferenceString(XmlLoopBeginBrick loopBeginBrick)
        {
            var sprite = XmlParserTempProjectHelper.Sprite;

            var script = XmlParserTempProjectHelper.Script;
            //TODO could this be dead code as since v93 there are no references like this?
                var count = 0;
                foreach (var brick in script.Bricks.Bricks)
                    if (brick is XmlRepeatBrick)
                    {
                        count++;
                        if ((brick == loopBeginBrick) && (count == 1))
                            return "../../repeatBrick";
                        else if (brick == loopBeginBrick)
                            return "../../repeatBrick[" + count + "]";
                    }
            return "";
        }

        private static string GetForeverLoopEndBrickReferenceString(XmlLoopEndBrick loopEndBrick)
        {
            var sprite = XmlParserTempProjectHelper.Sprite;

            var script = XmlParserTempProjectHelper.Script;
            //TODO could this be dead code as since v93 there are no references like this?
                var count = 0;
                foreach (var brick in script.Bricks.Bricks)
                    if (brick is XmlForeverLoopEndBrick)
                    {
                        count++;
                        if ((brick == loopEndBrick) && (count == 1))
                            return "../../loopEndlessBrick";
                        else if (brick == loopEndBrick)
                            return "../../loopEndlessBrick[" + count + "]";
                    }
            
            return "";
        }

        private static string GetRepeatLoopEndBrickReferenceString(XmlLoopEndBrick loopEndBrick)
        {
            var sprite = XmlParserTempProjectHelper.Sprite;

            var script = XmlParserTempProjectHelper.Script;
                //TODO could this be dead code as since v93 there are no references like this?
                var count = 0;
                foreach (var brick in script.Bricks.Bricks)
                    if (brick is XmlRepeatLoopEndBrick)
                    {
                        count++;
                        if ((brick == loopEndBrick) && (count == 1))
                            return "../../loopEndBrick";
                        else if (brick == loopEndBrick)
                            return "../../loopEndBrick[" + count + "]";
                    }
            
            return "";
        }

        private static string GetIfLogicBeginBrickReferenceString(XmlIfLogicBeginBrickReference ifLogicBeginBrickReference)
        {
            var ifLogicBeginBrick = ifLogicBeginBrickReference.IfLogicBeginBrick;

            var sprite = XmlParserTempProjectHelper.Sprite;

            var script = XmlParserTempProjectHelper.Script;

                var count = 0;
                foreach (var brick in script.Bricks.Bricks)
                    if (brick is XmlIfLogicBeginBrick)
                    {
                        count++;
                        if ((brick == ifLogicBeginBrick) && (count == 1))
                            return "../../ifLogicBeginBrick";
                        else if (brick == ifLogicBeginBrick)
                            return "../../ifLogicBeginBrick[" + count + "]";
                    }
            
            return "";
        }

        private static string GetIfLogicElseBrickReferenceString(XmlIfLogicElseBrickReference ifLogicElseBrickReference)
        {
            var ifLogicElseBrick = ifLogicElseBrickReference.IfLogicElseBrick;

            var sprite = XmlParserTempProjectHelper.Sprite;

            var script = XmlParserTempProjectHelper.Script;

                var count = 0;
                foreach (var brick in script.Bricks.Bricks)
                    if (brick is XmlIfLogicElseBrick)
                    {
                        count++;
                        if ((brick == ifLogicElseBrick) && (count == 1))
                            return "../../ifLogicElseBrick";
                        else if (brick == ifLogicElseBrick)
                            return "../../ifLogicElseBrick[" + count + "]";
                    }
            
            return "";
        }

        private static string GetIfLogicEndBrickReferenceString(XmlIfLogicEndBrickReference ifLogicEndBrickReference)
        {
            var ifLogicEndBrick = ifLogicEndBrickReference.IfLogicEndBrick;

            var sprite = XmlParserTempProjectHelper.Sprite;

            foreach (var script in sprite.Scripts.Scripts)
            {
                var count = 0;
                foreach (var brick in script.Bricks.Bricks)
                    if (brick is XmlIfLogicEndBrick)
                    {
                        count++;
                        if ((brick == ifLogicEndBrick) && (count == 1))
                            return "../../ifLogicEndBrick";
                        else if (brick == ifLogicEndBrick)
                            return "../../ifLogicEndBrick[" + count + "]";
                    }
            }
            return "";
        }


        public static XmlObjectNode GetReferenceObject(XmlObjectNode xmlObject, string reference)
        {
            if (reference == null)
                return null;

            if (xmlObject is XmlLookReference)
                return GetLookObject(xmlObject as XmlLookReference, reference);
            if (xmlObject is XmlSoundReference)
                return GetSoundObject(xmlObject as XmlSoundReference, reference);
            if (xmlObject is XmlSpriteReference)
                return GetSpriteObject(reference);
            if (xmlObject is XmlUserVariableReference)
                return GetUserVariableObject(xmlObject as XmlUserVariableReference, reference);
            if (xmlObject is XmlLoopBeginBrickReference)
            {
                if (reference.ToLower().Contains("forever"))
                    return GetForeverBrickObject(xmlObject as XmlLoopBeginBrickReference, reference);
                else
                    return GetRepeatBrickObject(xmlObject as XmlLoopBeginBrickReference, reference);
            }
            if (xmlObject is XmlLoopEndBrickReference)
            {
                if (reference.ToLower().Contains("endless"))
                    return GetForeverLoopEndBrickObject(xmlObject as XmlLoopEndBrickReference, reference);
                else
                    return GetRepeatLoopEndBrickObject(xmlObject as XmlLoopEndBrickReference, reference);
            }
            if (xmlObject is XmlIfLogicBeginBrickReference)
                return GetIfLogicBeginBrickObject(xmlObject as XmlIfLogicBeginBrickReference, reference);
            if (xmlObject is XmlIfLogicElseBrickReference)
                return GetIfLogicElseBrickObject(xmlObject as XmlIfLogicElseBrickReference, reference);
            if (xmlObject is XmlIfLogicEndBrickReference)
                return GetIfLogicEndBrickObject(xmlObject as XmlIfLogicEndBrickReference, reference);


            return null;
        }

        private static XmlObjectNode GetLookObject(XmlLookReference xmlLookReference, string reference)
        {
            var sprite = XmlParserTempProjectHelper.Sprite;

            foreach (var script in sprite.Scripts.Scripts)
                foreach (var brick in script.Bricks.Bricks)
                {
                    if (brick is XmlSetLookBrick)
                    {
                        var setLookBrick = brick as XmlSetLookBrick;
                        if (setLookBrick.XmlLookReference == xmlLookReference)
                        {
                            var count = 0;
                            if (reference.EndsWith("]"))
                            {
                                var splittetReference = reference.Split('[');
                                reference = reference.Split('[')[splittetReference.Count() - 1];
                                reference = reference.Split(']')[0];
                                count = Int32.Parse(reference) - 1;
                            }
                            return sprite.Looks.Looks[count];
                        }
                    }
                }
            return null;
        }

        private static XmlObjectNode GetSoundObject(XmlSoundReference xmlSoundReference, string reference)
        {
            var sprite = XmlParserTempProjectHelper.Sprite;

            foreach (var script in sprite.Scripts.Scripts)
                foreach (var brick in script.Bricks.Bricks)
                {
                    if (brick is XmlPlaySoundBrick)
                    {
                        var playSoundBrick = brick as XmlPlaySoundBrick;
                        if (playSoundBrick.XmlSoundReference == xmlSoundReference)
                        {
                            var count = 0;
                            if (reference.EndsWith("]"))
                            {
                                var splittetReference = reference.Split('[');
                                reference = reference.Split('[')[splittetReference.Count() - 1];
                                reference = reference.Split(']')[0];
                                count = Int32.Parse(reference) - 1;
                            }
                            return sprite.Sounds.Sounds[count];
                        }
                    }
                }
            return null;
        }

        private static XmlObjectNode GetSpriteObject(string reference)
        {
            var count = 0;
            if (reference.EndsWith("]"))
            {
                var splittetReference = reference.Split('[');
                reference = reference.Split('[')[splittetReference.Count() - 1];
                reference = reference.Split(']')[0];
                count = Int32.Parse(reference) - 1;
            }
            return XmlParserTempProjectHelper.Program.SpriteList.Sprites[count];
        }

        private static XmlObjectNode GetUserVariableObject(XmlUserVariableReference xmlUserVariableReference, string reference)
        {
            var tmpReference = reference;
            var found = false;
            var count = 0;

            var sprite = XmlParserTempProjectHelper.Sprite;

            foreach (var script in sprite.Scripts.Scripts)
                foreach (var brick in script.Bricks.Bricks)
                    if (/*brick is XmlSetVariableBrick ||*/ brick is XmlChangeVariableBrick)
                        if (/*brick is XmlSetVariableBrick && (brick as XmlSetVariableBrick).UserVariableReference == xmlUserVariableReference ||*/
                            brick is XmlChangeVariableBrick && (brick as XmlChangeVariableBrick).UserVariableReference == xmlUserVariableReference)
                        {
                            found = true;
                            if (tmpReference.EndsWith("]"))
                            {
                                var splittetReference = tmpReference.Split('[');
                                tmpReference = tmpReference.Split('[')[splittetReference.Count() - 1];
                                tmpReference = tmpReference.Split(']')[0];
                                count = Int32.Parse(tmpReference) - 1;
                            }
                            break;
                        }

            if (found)
            {
                //TODO: create constants
                if (reference.Contains("programVariableList"))
                {
                    return XmlParserTempProjectHelper.Program.VariableList.ProgramVariableList.UserVariables[count];
                }
                if (reference.Contains("objectVariableList"))
                {
                    var entries = XmlParserTempProjectHelper.Program.VariableList.ObjectVariableList.ObjectVariableEntries;
                    foreach (var entry in entries)
                    {
                        if (entry.Sprite == sprite)
                            return entry.VariableList.UserVariables[count];
                    }
                }
            }


            return null;
        }

        private static XmlObjectNode GetForeverBrickObject(XmlLoopBeginBrickReference loopBeginBrickReference, string reference)
        {
            var foreverBricks = new List<XmlBrick>();
            var sprite = XmlParserTempProjectHelper.Sprite;

                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is XmlForeverBrick)
                            foreverBricks.Add(brick as XmlForeverBrick);
                        if (brick is XmlLoopEndBrick)
                        {
                            var loopEndBrick = brick as XmlLoopEndBrick;
                            /*if (loopEndBrick.LoopBeginBrickReference == loopBeginBrickReference)
                            {
                                var count = 0;
                                if (reference.EndsWith("]"))
                                {
                                    var splittetReference = reference.Split('[');
                                    reference = reference.Split('[')[splittetReference.Count() - 1];
                                    reference = reference.Split(']')[0];
                                    count = Int32.Parse(reference) - 1;
                                }
                                return foreverBricks[count];
                            }*/
                        }
                    }
            return null;
        }

        private static XmlObjectNode GetRepeatBrickObject(XmlLoopBeginBrickReference loopBeginBrickReference, string reference)
        {
            var repeatBricks = new List<XmlBrick>();
            var sprite = XmlParserTempProjectHelper.Sprite;

            var script = XmlParserTempProjectHelper.Script;

                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is XmlRepeatBrick)
                            repeatBricks.Add(brick as XmlRepeatBrick);
                        if (brick is XmlLoopEndBrick)
                        {
                            var loopEndBrick = brick as XmlLoopEndBrick;
                            /*if (loopEndBrick.LoopBeginBrickReference == loopBeginBrickReference)
                            {
                                var count = 0;
                                if (reference.EndsWith("]"))
                                {
                                    var splittetReference = reference.Split('[');
                                    reference = reference.Split('[')[splittetReference.Count() - 1];
                                    reference = reference.Split(']')[0];
                                    count = Int32.Parse(reference) - 1;
                                }
                                return repeatBricks[count];
                            }*/
                        }
                    }
            return null;
        }

        private static XmlObjectNode GetForeverLoopEndBrickObject(XmlLoopEndBrickReference loopEndBrickReference, string reference)
        {
            bool found = false;
            var loopEndBricks = new List<XmlBrick>();

            var sprite = XmlParserTempProjectHelper.Sprite;

            var script = XmlParserTempProjectHelper.Script;

                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is XmlForeverBrick)
                        {
                            var foreverBrick = brick as XmlForeverBrick;
                            /*if (foreverBrick.LoopEndBrickReference == loopEndBrickReference)
                                found = true;*/
                        }
                        if (brick is XmlForeverLoopEndBrick)
                            loopEndBricks.Add(brick);
                    }
                    if (found)
                    {
                        var count = 0;
                        if (reference.EndsWith("]"))
                        {
                            var splittetReference = reference.Split('[');
                            reference = reference.Split('[')[splittetReference.Count() - 1];
                            reference = reference.Split(']')[0];
                            count = Int32.Parse(reference) - 1;
                        }
                        return loopEndBricks[count];
                    }
                
            return null;
        }

        private static XmlObjectNode GetRepeatLoopEndBrickObject(XmlLoopEndBrickReference loopEndBrickReference, string reference)
        {
            bool found = false;
            var loopEndBricks = new List<XmlBrick>();

            var sprite = XmlParserTempProjectHelper.Sprite;

            var script = XmlParserTempProjectHelper.Script;
   
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is XmlRepeatBrick)
                        {
                            var repeatBrick = brick as XmlRepeatBrick;
                            /*if (repeatBrick.LoopEndBrickReference == loopEndBrickReference)
                                found = true;*/
                        }
                        if (brick is XmlRepeatLoopEndBrick)
                            loopEndBricks.Add(brick);
                    }
                    if (found)
                    {
                        var count = 0;
                        if (reference.EndsWith("]"))
                        {
                            var splittetReference = reference.Split('[');
                            reference = reference.Split('[')[splittetReference.Count() - 1];
                            reference = reference.Split(']')[0];
                            count = Int32.Parse(reference) - 1;
                        }
                        return loopEndBricks[count];
                    }
                
            return null;
        }

        private static XmlObjectNode GetIfLogicBeginBrickObject(XmlIfLogicBeginBrickReference ifLogicBeginBrickReference, string reference)
        {
            bool found = false;
            var ifLogicBeginBricks = new List<XmlBrick>();

            var sprite = XmlParserTempProjectHelper.Sprite;

                var script = XmlParserTempProjectHelper.Script;
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is XmlIfLogicElseBrick)
                        {
                            var ifLogicElseBrick = brick as XmlIfLogicElseBrick;
                            if (ifLogicElseBrick.IfLogicBeginBrickReference == ifLogicBeginBrickReference)
                                found = true;
                        }
                        if (brick is XmlIfLogicEndBrick)
                        {
                            var ifLogicEndBrick = brick as XmlIfLogicEndBrick;
                            if (ifLogicEndBrick.IfLogicBeginBrickReference == ifLogicBeginBrickReference)
                                found = true;
                        }
                        if (brick is XmlIfLogicBeginBrick)
                            ifLogicBeginBricks.Add(brick);
                    }
                    if (found)
                    {
                        var count = 0;
                        if (reference.EndsWith("]"))
                        {
                            var splittetReference = reference.Split('[');
                            reference = reference.Split('[')[splittetReference.Count() - 1];
                            reference = reference.Split(']')[0];
                            count = Int32.Parse(reference) - 1;
                        }
                        return ifLogicBeginBricks[count];
                    }
                
            return null;
        }

        private static XmlObjectNode GetIfLogicElseBrickObject(XmlIfLogicElseBrickReference ifLogicElseBrickReference, string reference)
        {
            bool found = false;
            var ifLogicElseBricks = new List<XmlBrick>();

            var sprite = XmlParserTempProjectHelper.Sprite;

                var script = XmlParserTempProjectHelper.Script;

                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is XmlIfLogicBeginBrick)
                        {
                            var ifLogicBeginBrick = brick as XmlIfLogicBeginBrick;
                            if (ifLogicBeginBrick.IfLogicElseBrickReference == ifLogicElseBrickReference)
                                found = true;
                        }
                        if (brick is XmlIfLogicEndBrick)
                        {
                            var ifLogicEndBrick = brick as XmlIfLogicEndBrick;
                            if (ifLogicEndBrick.IfLogicElseBrickReference == ifLogicElseBrickReference)
                                found = true;
                        }
                        if (brick is XmlIfLogicElseBrick)
                            ifLogicElseBricks.Add(brick);
                    }
                    if (found)
                    {
                        var count = 0;
                        if (reference.EndsWith("]"))
                        {
                            var splittetReference = reference.Split('[');
                            reference = reference.Split('[')[splittetReference.Count() - 1];
                            reference = reference.Split(']')[0];
                            count = Int32.Parse(reference) - 1;
                        }
                        return ifLogicElseBricks[count];
                    }
                
            return null;
        }

        private static XmlObjectNode GetIfLogicEndBrickObject(XmlIfLogicEndBrickReference ifLogicEndBrickReference, string reference)
        {
            bool found = false;
            var ifLogicEndBricks = new List<XmlBrick>();

            var sprite = XmlParserTempProjectHelper.Sprite;

            var script = XmlParserTempProjectHelper.Script;

                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is XmlIfLogicBeginBrick)
                        {
                            var ifLogicBeginBrick = brick as XmlIfLogicBeginBrick;
                            if (ifLogicBeginBrick.IfLogicEndBrickReference == ifLogicEndBrickReference)
                                found = true;
                        }
                        if (brick is XmlIfLogicElseBrick)
                        {
                            var ifLogicElseBrick = brick as XmlIfLogicElseBrick;
                            if (ifLogicElseBrick.IfLogicEndBrickReference == ifLogicEndBrickReference)
                                found = true;
                        }
                        if (brick is XmlIfLogicEndBrick)
                            ifLogicEndBricks.Add(brick);
                    }
                    if (found)
                    {
                        var count = 0;
                        if (reference.EndsWith("]"))
                        {
                            var splittetReference = reference.Split('[');
                            reference = reference.Split('[')[splittetReference.Count() - 1];
                            reference = reference.Split(']')[0];
                            count = Int32.Parse(reference) - 1;
                        }
                        return ifLogicEndBricks[count];
                    }
                
            return null;
        }


        public static void UpdateReferencesAfterCopy(XmlSprite oldSprite, XmlSprite newSprite)
        {
            var scriptCount = 0;
            foreach (var script in oldSprite.Scripts.Scripts)
            {
                var brickCount = 0;
                foreach (var brick in script.Bricks.Bricks)
                {
                    if (brick is XmlChangeVariableBrick || brick is XmlSetVariableBrick)
                        UpdateVariableReference(oldSprite, newSprite, scriptCount, brickCount);
                    else if (brick is XmlSetLookBrick)
                        UpdateLookReference(oldSprite, newSprite, scriptCount, brickCount);
                    else if (brick is XmlForeverBrick || brick is XmlRepeatBrick)
                        UpdateLoopEndBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                    else if (brick is XmlIfLogicBeginBrick)
                    {
                        UpdateIfLogicElseBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                        UpdateIfLogicEndBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                    }
                    else if (brick is XmlIfLogicElseBrick)
                    {
                        UpdateIfLogicBeginBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                        UpdateIfLogicEndBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                    }
                    else if (brick is XmlIfLogicEndBrick)
                    {
                        UpdateIfLogicBeginBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                        UpdateIfLogicElseBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                    }
                    else if (brick is XmlLoopEndBrick)
                        UpdateLoopBeginBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                    else if (brick is XmlPlaySoundBrick)
                        UpdateSoundReference(oldSprite, newSprite, scriptCount, brickCount);

                    brickCount++;
                }

                scriptCount++;
            }
        }

        private static void UpdateLookReference(XmlSprite oldSprite, XmlSprite newSprite, int scriptCount, int brickCount)
        {
            var oldLookReference = (oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlSetLookBrick).XmlLookReference;
            var newLookReference = (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlSetLookBrick).XmlLookReference;

            var lookCount = 0;
            foreach (var look in oldSprite.Looks.Looks)
            {
                if (look == oldLookReference.Look)
                {
                    newLookReference.Look = newSprite.Looks.Looks[lookCount];
                    return;
                }

                lookCount++;
            }
        }

        private static void UpdateSoundReference(XmlSprite oldSprite, XmlSprite newSprite, int scriptCount, int brickCount)
        {
            var oldSoundReference = (oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlPlaySoundBrick).XmlSoundReference;
            var newSoundReference = (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlPlaySoundBrick).XmlSoundReference;

            var soundCount = 0;
            foreach (var sound in oldSprite.Sounds.Sounds)
            {
                if (sound == oldSoundReference.Sound)
                {
                    newSoundReference.Sound = newSprite.Sounds.Sounds[soundCount];
                    return;
                }

                soundCount++;
            }
        }

        private static void UpdateVariableReference(XmlSprite oldSprite, XmlSprite newSprite, int scriptCount, int brickCount)
        {
            XmlUserVariableReference oldVariableReference;
            XmlUserVariableReference newVariableReference;

            var oldBrick = oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount];
            /*if (oldBrick is XmlChangeVariableBrick)
            {*/
                oldVariableReference = (oldBrick as XmlChangeVariableBrick).UserVariableReference;
                newVariableReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlChangeVariableBrick)
                        .UserVariableReference;
            /*}
            else
            {
                oldVariableReference = (oldBrick as XmlSetVariableBrick).UserVariableReference;
                newVariableReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlSetVariableBrick)
                        .UserVariableReference;
            }*/

            var entries = XmlParserTempProjectHelper.Program.VariableList.ObjectVariableList.ObjectVariableEntries;
            foreach (var oldEntry in entries)
                if (oldEntry.Sprite == oldSprite)
                {
                    var variableCount = 0;
                    foreach (var variable in oldEntry.VariableList.UserVariables)
                    {
                        if (variable == oldVariableReference.UserVariable)
                            foreach (var newEntry in entries)
                            {
                                if (newEntry.Sprite == newSprite)
                                {
                                    newVariableReference.UserVariable =
                                        newEntry.VariableList.UserVariables[variableCount];

                                    return;
                                }
                            }
                        variableCount++;
                    }
                }
        }

        private static void UpdateLoopBeginBrickReference(XmlSprite oldSprite, XmlSprite newSprite, int scriptCount, int brickCount)
        {
            //var oldLoopBeginBrickReference = (oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlLoopEndBrick).LoopBeginBrickReference;
            //var newLoopBeginBrickReference = (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlLoopEndBrick).LoopBeginBrickReference;

            var count = 0;
            foreach (var brick in oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks)
            {
                /*if (brick == oldLoopBeginBrickReference.LoopBeginBrick)
                {
                    newLoopBeginBrickReference.LoopBeginBrick = newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[count] as XmlLoopBeginBrick;
                    return;
                }*/
                count++;
            }
        }

        private static void UpdateLoopEndBrickReference(XmlSprite oldSprite, XmlSprite newSprite, int scriptCount, int brickCount)
        {
            XmlLoopEndBrickReference oldLoopEndBrickReference;
            XmlLoopEndBrickReference newLoopEndBrickReference;

            var oldBrick = oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount];
            if (oldBrick is XmlForeverBrick)
            {
                //oldLoopEndBrickReference = (oldBrick as XmlForeverBrick).LoopEndBrickReference;
                /*newLoopEndBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlForeverBrick)
                        .LoopEndBrickReference;*/
            }
            else
            {
               //oldLoopEndBrickReference = (oldBrick as XmlRepeatBrick).LoopEndBrickReference;
                /* newLoopEndBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlRepeatBrick)
                        .LoopEndBrickReference;*/
            }

            var count = 0;
            foreach (var brick in oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks)
            {
                count++;
            }
        }

        private static void UpdateIfLogicBeginBrickReference(XmlSprite oldSprite, XmlSprite newSprite, int scriptCount, int brickCount)
        {
            XmlIfLogicBeginBrickReference oldIfLogicBeginBrickReference;
            XmlIfLogicBeginBrickReference newIfLogicBeginBrickReference;

            var oldBrick = oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount];
            if (oldBrick is XmlIfLogicElseBrick)
            {
                oldIfLogicBeginBrickReference = (oldBrick as XmlIfLogicElseBrick).IfLogicBeginBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlIfLogicElseBrick)
                        .IfLogicBeginBrickReference;
            }
            else
            {
                oldIfLogicBeginBrickReference = (oldBrick as XmlIfLogicEndBrick).IfLogicBeginBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlIfLogicEndBrick)
                        .IfLogicBeginBrickReference;
            }

            var count = 0;
            foreach (var brick in oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks)
            {
                if (brick == oldIfLogicBeginBrickReference.IfLogicBeginBrick)
                {
                    newIfLogicBeginBrickReference.IfLogicBeginBrick = newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[count] as XmlIfLogicBeginBrick;
                    return;
                }
                count++;
            }
        }

        private static void UpdateIfLogicElseBrickReference(XmlSprite oldSprite, XmlSprite newSprite, int scriptCount, int brickCount)
        {
            XmlIfLogicElseBrickReference oldIfLogicBeginBrickReference;
            XmlIfLogicElseBrickReference newIfLogicBeginBrickReference;

            var oldBrick = oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount];
            if (oldBrick is XmlIfLogicBeginBrick)
            {
                oldIfLogicBeginBrickReference = (oldBrick as XmlIfLogicBeginBrick).IfLogicElseBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlIfLogicBeginBrick)
                        .IfLogicElseBrickReference;
            }
            else
            {
                oldIfLogicBeginBrickReference = (oldBrick as XmlIfLogicEndBrick).IfLogicElseBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlIfLogicEndBrick)
                        .IfLogicElseBrickReference;
            }

            var count = 0;
            foreach (var brick in oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks)
            {
                if (brick == oldIfLogicBeginBrickReference.IfLogicElseBrick)
                {
                    newIfLogicBeginBrickReference.IfLogicElseBrick = newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[count] as XmlIfLogicElseBrick;
                    return;
                }
                count++;
            }
        }

        private static void UpdateIfLogicEndBrickReference(XmlSprite oldSprite, XmlSprite newSprite, int scriptCount, int brickCount)
        {
            XmlIfLogicEndBrickReference oldIfLogicBeginBrickReference;
            XmlIfLogicEndBrickReference newIfLogicBeginBrickReference;

            var oldBrick = oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount];
            if (oldBrick is XmlIfLogicBeginBrick)
            {
                oldIfLogicBeginBrickReference = (oldBrick as XmlIfLogicBeginBrick).IfLogicEndBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlIfLogicBeginBrick)
                        .IfLogicEndBrickReference;
            }
            else
            {
                oldIfLogicBeginBrickReference = (oldBrick as XmlIfLogicElseBrick).IfLogicEndBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as XmlIfLogicElseBrick)
                        .IfLogicEndBrickReference;
            }

            var count = 0;
            foreach (var brick in oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks)
            {
                if (brick == oldIfLogicBeginBrickReference.IfLogicEndBrick)
                {
                    newIfLogicBeginBrickReference.IfLogicEndBrick = newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[count] as XmlIfLogicEndBrick;
                    return;
                }
                count++;
            }
        }
    }
}
