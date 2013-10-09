using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.CatrobatObjects.Bricks;
using Catrobat.Core.CatrobatObjects.Costumes;
using Catrobat.Core.CatrobatObjects.Scripts;
using Catrobat.Core.CatrobatObjects.Sounds;
using Catrobat.Core.CatrobatObjects.Variables;
using Catrobat.Core.Services.Common;

namespace Catrobat.Core.Utilities.Helpers
{
    public class ReferenceHelper
    {
        public static string GetReferenceString(DataObject referenceObject)
        {
            if (referenceObject is CostumeReference)
                return GetCostumeReferenceString((referenceObject as CostumeReference).Costume);
            if (referenceObject is SoundReference)
                return GetSoundReferenceString((referenceObject as SoundReference).Sound);
            if (referenceObject is SpriteReference)
                return GetSpriteReferenceString((referenceObject as SpriteReference));
            if (referenceObject is UserVariableReference)
                return GetVariableReferenceString(referenceObject as UserVariableReference);
            if (referenceObject is LoopBeginBrickReference)
            {
                var loopBeginBrickReference = referenceObject as LoopBeginBrickReference;
                if (loopBeginBrickReference.Class == "forever")
                    return GetForeverBrickReferenceString(loopBeginBrickReference.LoopBeginBrick);
                if (loopBeginBrickReference.Class == "repeat")
                    return GetRepeatBrickReferenceString(loopBeginBrickReference.LoopBeginBrick);
            }
            if (referenceObject is LoopEndBrickReference)
                return GetLoopEndBrickReferenceString(referenceObject as LoopEndBrickReference);
            if (referenceObject is IfLogicBeginBrickReference)
                return GetIfLogicBeginBrickReferenceString(referenceObject as IfLogicBeginBrickReference);
            if (referenceObject is IfLogicElseBrickReference)
                return GetIfLogicElseBrickReferenceString(referenceObject as IfLogicElseBrickReference);
            if (referenceObject is IfLogicEndBrickReference)
                return GetIfLogicEndBrickReferenceString(referenceObject as IfLogicEndBrickReference);

            return "";
        }

        private static string GetCostumeReferenceString(Costume costume)
        {
            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
            {
                var count = 0;
                foreach (var tempCostume in sprite.Costumes.Costumes)
                {
                    count++;
                    if (tempCostume == costume)
                        return "../../../../../lookList/look[" + count + "]";
                }
            }
            return "";
        }

        private static string GetSoundReferenceString(Sound sound)
        {
            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
            {
                var count = 0;
                foreach (var tempSound in sprite.Sounds.Sounds)
                {
                    count++;
                    if (tempSound == sound)
                        return "../../../../../soundList/sound[" + count + "]";
                }
            }
            return "";
        }

        private static string GetSpriteReferenceString(SpriteReference spriteReference)
        {
            var sprite = spriteReference.Sprite;
            var count = 0;

            foreach (var tempSprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
            {
                count++;
                if (tempSprite == sprite)
                    break;
            }

            foreach (var tempSprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in tempSprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is PointToBrick)
                        {
                            var pointToBrick = brick as PointToBrick;
                            if (pointToBrick.PointedSpriteReference == spriteReference)
                                return "../../../../../../object[" + count + "]";
                        }
                    }

            foreach (var entry in XmlParserTempProjectHelper.Project.VariableList.ObjectVariableList.ObjectVariableEntries)
            {
                if (entry.SpriteReference == spriteReference)
                    return "../../../../objectList/object[" + count + "]";
            }

            return "";
        }

        private static string GetVariableReferenceString(UserVariableReference userVariableReference)
        {
            var userVariable = userVariableReference.UserVariable;
            var entryCount = 0;
            foreach (var entry in XmlParserTempProjectHelper.Project.VariableList.ObjectVariableList.ObjectVariableEntries)
            {
                entryCount++;
                var userVariableCount = 0;
                foreach (var tempUserVariable in entry.VariableList.UserVariables)
                {
                    userVariableCount++;
                    if (tempUserVariable == userVariable)
                        return "../../../../../variables/objectVariableList/entry[" + entryCount +
                               "]/list/userVariable[" + userVariableCount + "]";
                }
            }

            var count = 0;
            foreach (var tempUserVariable in XmlParserTempProjectHelper.Project.VariableList.ProgramVariableList.UserVariables)
            {
                count++;
                if (tempUserVariable == userVariable)
                    return "../../../../../variables/programVariableList/userVariable[" + count + "]";
            }

            return "";
        }

        private static string GetForeverBrickReferenceString(LoopBeginBrick loopBeginBrick)
        {
            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    var count = 0;
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is ForeverBrick)
                        {
                            count++;
                            if (brick == loopBeginBrick)
                                return "../../foreverBrick[" + count + "]";
                        }
                }
            return "";
        }

        private static string GetRepeatBrickReferenceString(LoopBeginBrick loopBeginBrick)
        {
            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    var count = 0;
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is RepeatBrick)
                        {
                            count++;
                            if (brick == loopBeginBrick)
                                return "../../repeatBrick[" + count + "]";
                        }
                }
            return "";
        }

        private static string GetLoopEndBrickReferenceString(LoopEndBrickReference loopEndBrickRef)
        {
            var loopEndBrick = loopEndBrickRef.LoopEndBrick;

            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    var count = 0;
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is LoopEndBrick)
                        {
                            count++;
                            if (brick == loopEndBrick)
                                return "../../loopEndBrick[" + count + "]";
                        }
                }
            return "";
        }

        private static string GetIfLogicBeginBrickReferenceString(IfLogicBeginBrickReference ifLogicBeginBrickReference)
        {
            var ifLogicBeginBrick = ifLogicBeginBrickReference.IfLogicBeginBrick;

            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    var count = 0;
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is IfLogicBeginBrick)
                        {
                            count++;
                            if (brick == ifLogicBeginBrick)
                                return "../../ifLogicBeginBrick[" + count + "]";
                        }
                }
            return "";
        }

        private static string GetIfLogicElseBrickReferenceString(IfLogicElseBrickReference ifLogicElseBrickReference)
        {
            var ifLogicElseBrick = ifLogicElseBrickReference.IfLogicElseBrick;

            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    var count = 0;
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is IfLogicElseBrick)
                        {
                            count++;
                            if (brick == ifLogicElseBrick)
                                return "../../ifLogicElseBrick[" + count + "]";
                        }
                }
            return "";
        }

        private static string GetIfLogicEndBrickReferenceString(IfLogicEndBrickReference ifLogicEndBrickReference)
        {
            var ifLogicEndBrick = ifLogicEndBrickReference.IfLogicEndBrick;

            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    var count = 0;
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is IfLogicEndBrick)
                        {
                            count++;
                            if (brick == ifLogicEndBrick)
                                return "../../ifLogicEndBrick[" + count + "]";
                        }
                }
            return "";
        }


        public static DataObject GetReferenceObject(DataObject dataObject, string reference)
        {
            if (dataObject is CostumeReference)
                return GetCostumeObject(dataObject as CostumeReference, reference);
            if (dataObject is SoundReference)
                return GetSoundObject(dataObject as SoundReference, reference);
            if (dataObject is SpriteReference)
                return GetSpriteObject(reference);
            if (dataObject is UserVariableReference)
                return GetUserVariableObject(dataObject as UserVariableReference, reference);
            if (dataObject is LoopBeginBrickReference)
            {
                var loopBeginBrickReference = dataObject as LoopBeginBrickReference;
                if (loopBeginBrickReference.Class == "forever")
                    return GetForeverBrickObject(loopBeginBrickReference, reference);
                if (loopBeginBrickReference.Class == "repeat")
                    return GetRepeatBrickObject(loopBeginBrickReference, reference);
            }
            if (dataObject is LoopEndBrickReference)
                return GetLoopEndBrickObject(dataObject as LoopEndBrickReference, reference);
            if (dataObject is IfLogicBeginBrickReference)
                return GetIfLogicBeginBrickObject(dataObject as IfLogicBeginBrickReference, reference);
            if (dataObject is IfLogicElseBrickReference)
                return GetIfLogicElseBrickObject(dataObject as IfLogicElseBrickReference, reference);
            if (dataObject is IfLogicEndBrickReference)
                return GetIfLogicEndBrickObject(dataObject as IfLogicEndBrickReference, reference);


            return null;
        }

        private static DataObject GetCostumeObject(CostumeReference costumeReference, string reference)
        {
            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is SetCostumeBrick)
                        {
                            var setCostumeBrick = brick as SetCostumeBrick;
                            if (setCostumeBrick.CostumeReference == costumeReference)
                            {
                                var count = 0;
                                if (reference.EndsWith("]"))
                                {
                                    var splittetReference = reference.Split('[');
                                    reference = reference.Split('[')[splittetReference.Count() - 1];
                                    reference = reference.Split(']')[0];
                                    count = Int32.Parse(reference) - 1;
                                }
                                return sprite.Costumes.Costumes[count];
                            }
                        }
                    }
            return null;
        }

        private static DataObject GetSoundObject(SoundReference soundReference, string reference)
        {
            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is PlaySoundBrick)
                        {
                            var playSoundBrick = brick as PlaySoundBrick;
                            if (playSoundBrick.SoundReference == soundReference)
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

        private static DataObject GetSpriteObject(string reference)
        {
            var count = 0;
            if (reference.EndsWith("]"))
            {
                var splittetReference = reference.Split('[');
                reference = reference.Split('[')[splittetReference.Count() - 1];
                reference = reference.Split(']')[0];
                count = Int32.Parse(reference) - 1;
            }
            return XmlParserTempProjectHelper.Project.SpriteList.Sprites[count];
        }

        private static DataObject GetUserVariableObject(UserVariableReference userVariableReference, string reference)
        {
            bool found = false;
            var count = 0;

            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
            {
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is SetVariableBrick || brick is ChangeVariableBrick)
                            if (brick is SetVariableBrick && (brick as SetVariableBrick).UserVariableReference == userVariableReference ||
                                brick is ChangeVariableBrick && (brick as ChangeVariableBrick).UserVariableReference == userVariableReference)
                            {
                                found = true;
                                if (reference.EndsWith("]"))
                                {
                                    var splittetReference = reference.Split('[');
                                    reference = reference.Split('[')[splittetReference.Count() - 1];
                                    reference = reference.Split(']')[0];
                                    count = Int32.Parse(reference) - 1;
                                }
                                break;
                            }

                if (found)
                {
                    var entries = XmlParserTempProjectHelper.Project.VariableList.ObjectVariableList.ObjectVariableEntries;
                    foreach (var entry in entries)
                    {
                        if (entry.Sprite == sprite)
                            return entry.VariableList.UserVariables[count];
                    }
                }
            }


            return null;
        }

        private static DataObject GetForeverBrickObject(LoopBeginBrickReference loopBeginBrickReference, string reference)
        {
            var foreverBricks = new List<Brick>();
            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is ForeverBrick)
                            foreverBricks.Add(brick as ForeverBrick);
                        if (brick is LoopEndBrick)
                        {
                            var loopEndBrick = brick as LoopEndBrick;
                            if (loopEndBrick.LoopBeginBrickReference == loopBeginBrickReference)
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
                            }
                        }
                    }
            return null;
        }

        private static DataObject GetRepeatBrickObject(LoopBeginBrickReference loopBeginBrickReference, string reference)
        {
            var repeatBricks = new List<Brick>();
            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is RepeatBrick)
                            repeatBricks.Add(brick as RepeatBrick);
                        if (brick is LoopEndBrick)
                        {
                            var loopEndBrick = brick as LoopEndBrick;
                            if (loopEndBrick.LoopBeginBrickReference == loopBeginBrickReference)
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
                            }
                        }
                    }
            return null;
        }

        private static DataObject GetLoopEndBrickObject(LoopEndBrickReference loopEndBrickReference, string reference)
        {
            bool found = false;
            var loopEndBricks = new List<Brick>();

            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is RepeatBrick)
                        {
                            var repeatBrick = brick as RepeatBrick;
                            if (repeatBrick.LoopEndBrickReference == loopEndBrickReference)
                                found = true;
                        }
                        if (brick is ForeverBrick)
                        {
                            var foreverBrick = brick as ForeverBrick;
                            if (foreverBrick.LoopEndBrickReference == loopEndBrickReference)
                                found = true;
                        }
                        if (brick is LoopEndBrick)
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
                }
            return null;
        }

        private static DataObject GetIfLogicBeginBrickObject(IfLogicBeginBrickReference ifLogicBeginBrickReference, string reference)
        {
            bool found = false;
            var ifLogicBeginBricks = new List<Brick>();

            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is IfLogicElseBrick)
                        {
                            var ifLogicElseBrick = brick as IfLogicElseBrick;
                            if (ifLogicElseBrick.IfLogicBeginBrickReference == ifLogicBeginBrickReference)
                                found = true;
                        }
                        if (brick is IfLogicEndBrick)
                        {
                            var ifLogicEndBrick = brick as IfLogicEndBrick;
                            if (ifLogicEndBrick.IfLogicBeginBrickReference == ifLogicBeginBrickReference)
                                found = true;
                        }
                        if (brick is IfLogicBeginBrick)
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
                }
            return null;
        }

        private static DataObject GetIfLogicElseBrickObject(IfLogicElseBrickReference ifLogicElseBrickReference, string reference)
        {
            bool found = false;
            var ifLogicElseBricks = new List<Brick>();

            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is IfLogicBeginBrick)
                        {
                            var ifLogicBeginBrick = brick as IfLogicBeginBrick;
                            if (ifLogicBeginBrick.IfLogicElseBrickReference == ifLogicElseBrickReference)
                                found = true;
                        }
                        if (brick is IfLogicEndBrick)
                        {
                            var ifLogicEndBrick = brick as IfLogicEndBrick;
                            if (ifLogicEndBrick.IfLogicElseBrickReference == ifLogicElseBrickReference)
                                found = true;
                        }
                        if (brick is IfLogicElseBrick)
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
                }
            return null;
        }

        private static DataObject GetIfLogicEndBrickObject(IfLogicEndBrickReference ifLogicEndBrickReference, string reference)
        {
            bool found = false;
            var ifLogicEndBricks = new List<Brick>();

            foreach (var sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is IfLogicBeginBrick)
                        {
                            var ifLogicBeginBrick = brick as IfLogicBeginBrick;
                            if (ifLogicBeginBrick.IfLogicEndBrickReference == ifLogicEndBrickReference)
                                found = true;
                        }
                        if (brick is IfLogicElseBrick)
                        {
                            var ifLogicElseBrick = brick as IfLogicElseBrick;
                            if (ifLogicElseBrick.IfLogicEndBrickReference == ifLogicEndBrickReference)
                                found = true;
                        }
                        if (brick is IfLogicEndBrick)
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
                }
            return null;
        }


        public static void UpdateReferencesAfterCopy(Sprite oldSprite, Sprite newSprite)
        {
            var scriptCount = 0;
            foreach (var script in oldSprite.Scripts.Scripts)
            {
                var brickCount = 0;
                foreach (var brick in script.Bricks.Bricks)
                {
                    if (brick is ChangeVariableBrick || brick is SetVariableBrick)
                        UpdateVariableReference(oldSprite, newSprite, scriptCount, brickCount);
                    else if (brick is SetCostumeBrick)
                        UpdateCostumeReference(oldSprite, newSprite, scriptCount, brickCount);
                    else if (brick is ForeverBrick || brick is RepeatBrick)
                        UpdateLoopEndBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                    else if (brick is IfLogicBeginBrick)
                    {
                        UpdateIfLogicElseBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                        UpdateIfLogicEndBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                    }
                    else if (brick is IfLogicElseBrick)
                    {
                        UpdateIfLogicBeginBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                        UpdateIfLogicEndBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                    }
                    else if (brick is IfLogicEndBrick)
                    {
                        UpdateIfLogicBeginBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                        UpdateIfLogicElseBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                    }
                    else if(brick is LoopEndBrick)
                        UpdateLoopBeginBrickReference(oldSprite, newSprite, scriptCount, brickCount);
                    else if(brick is PlaySoundBrick)
                        UpdateSoundReference(oldSprite, newSprite, scriptCount, brickCount);

                    brickCount++;
                }

                scriptCount++;
            }
        }

        private static void UpdateCostumeReference(Sprite oldSprite, Sprite newSprite, int scriptCount, int brickCount)
        {
            var oldCostumeReference = (oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as SetCostumeBrick).CostumeReference;
            var newCostumeReference = (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as SetCostumeBrick).CostumeReference;

            var costumeCount = 0;
            foreach (var costume in oldSprite.Costumes.Costumes)
            {
                if (costume == oldCostumeReference.Costume)
                {
                    newCostumeReference.Costume = newSprite.Costumes.Costumes[costumeCount];
                    return;
                }

                costumeCount++;
            }
        }

        private static void UpdateSoundReference(Sprite oldSprite, Sprite newSprite, int scriptCount, int brickCount)
        {
            var oldSoundReference = (oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as PlaySoundBrick).SoundReference;
            var newSoundReference = (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as PlaySoundBrick).SoundReference;

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

        private static void UpdateVariableReference(Sprite oldSprite, Sprite newSprite, int scriptCount, int brickCount)
        {
            UserVariableReference oldVariableReference;
            UserVariableReference newVariableReference;

            var oldBrick = oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount];
            if (oldBrick is ChangeVariableBrick)
            {
                oldVariableReference = (oldBrick as ChangeVariableBrick).UserVariableReference;
                newVariableReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as ChangeVariableBrick)
                        .UserVariableReference;
            }
            else
            {
                oldVariableReference = (oldBrick as SetVariableBrick).UserVariableReference;
                newVariableReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as SetVariableBrick)
                        .UserVariableReference;
            }

            var entries = XmlParserTempProjectHelper.Project.VariableList.ObjectVariableList.ObjectVariableEntries;
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

        private static void UpdateLoopBeginBrickReference(Sprite oldSprite, Sprite newSprite, int scriptCount, int brickCount)
        {
            var oldLoopBeginBrickReference = (oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as LoopEndBrick).LoopBeginBrickReference;
            var newLoopBeginBrickReference = (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as LoopEndBrick).LoopBeginBrickReference;

            var count = 0;
            foreach (var brick in oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks)
            {
                if (brick == oldLoopBeginBrickReference.LoopBeginBrick)
                {
                    newLoopBeginBrickReference.LoopBeginBrick = newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[count] as LoopBeginBrick;
                    return;
                }
                count++;
            }
        }

        private static void UpdateLoopEndBrickReference(Sprite oldSprite, Sprite newSprite, int scriptCount, int brickCount)
        {
            LoopEndBrickReference oldLoopEndBrickReference;
            LoopEndBrickReference newLoopEndBrickReference;

            var oldBrick = oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount];
            if (oldBrick is ForeverBrick)
            {
                oldLoopEndBrickReference = (oldBrick as ForeverBrick).LoopEndBrickReference;
                newLoopEndBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as ForeverBrick)
                        .LoopEndBrickReference;
            }
            else
            {
                oldLoopEndBrickReference = (oldBrick as RepeatBrick).LoopEndBrickReference;
                newLoopEndBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as RepeatBrick)
                        .LoopEndBrickReference;
            }

            var count = 0;
            foreach (var brick in oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks)
            {
                if (brick == oldLoopEndBrickReference.LoopEndBrick)
                {
                    newLoopEndBrickReference.LoopEndBrick = newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[count] as LoopEndBrick;
                    return;
                }
                count++;
            }
        }

        private static void UpdateIfLogicBeginBrickReference(Sprite oldSprite, Sprite newSprite, int scriptCount, int brickCount)
        {
            IfLogicBeginBrickReference oldIfLogicBeginBrickReference;
            IfLogicBeginBrickReference newIfLogicBeginBrickReference;

            var oldBrick = oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount];
            if (oldBrick is IfLogicElseBrick)
            {
                oldIfLogicBeginBrickReference = (oldBrick as IfLogicElseBrick).IfLogicBeginBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as IfLogicElseBrick)
                        .IfLogicBeginBrickReference;
            }
            else
            {
                oldIfLogicBeginBrickReference = (oldBrick as IfLogicEndBrick).IfLogicBeginBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as IfLogicEndBrick)
                        .IfLogicBeginBrickReference;
            }

            var count = 0;
            foreach (var brick in oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks)
            {
                if (brick == oldIfLogicBeginBrickReference.IfLogicBeginBrick)
                {
                    newIfLogicBeginBrickReference.IfLogicBeginBrick = newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[count] as IfLogicBeginBrick;
                    return;
                }
                count++;
            }
        }

        private static void UpdateIfLogicElseBrickReference(Sprite oldSprite, Sprite newSprite, int scriptCount, int brickCount)
        {
            IfLogicElseBrickReference oldIfLogicBeginBrickReference;
            IfLogicElseBrickReference newIfLogicBeginBrickReference;

            var oldBrick = oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount];
            if (oldBrick is IfLogicBeginBrick)
            {
                oldIfLogicBeginBrickReference = (oldBrick as IfLogicBeginBrick).IfLogicElseBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as IfLogicBeginBrick)
                        .IfLogicElseBrickReference;
            }
            else
            {
                oldIfLogicBeginBrickReference = (oldBrick as IfLogicEndBrick).IfLogicElseBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as IfLogicEndBrick)
                        .IfLogicElseBrickReference;
            }

            var count = 0;
            foreach (var brick in oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks)
            {
                if (brick == oldIfLogicBeginBrickReference.IfLogicElseBrick)
                {
                    newIfLogicBeginBrickReference.IfLogicElseBrick = newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[count] as IfLogicElseBrick;
                    return;
                }
                count++;
            }
        }

        private static void UpdateIfLogicEndBrickReference(Sprite oldSprite, Sprite newSprite, int scriptCount, int brickCount)
        {
            IfLogicEndBrickReference oldIfLogicBeginBrickReference;
            IfLogicEndBrickReference newIfLogicBeginBrickReference;

            var oldBrick = oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount];
            if (oldBrick is IfLogicBeginBrick)
            {
                oldIfLogicBeginBrickReference = (oldBrick as IfLogicBeginBrick).IfLogicEndBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as IfLogicBeginBrick)
                        .IfLogicEndBrickReference;
            }
            else
            {
                oldIfLogicBeginBrickReference = (oldBrick as IfLogicElseBrick).IfLogicEndBrickReference;
                newIfLogicBeginBrickReference =
                    (newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[brickCount] as IfLogicElseBrick)
                        .IfLogicEndBrickReference;
            }

            var count = 0;
            foreach (var brick in oldSprite.Scripts.Scripts[scriptCount].Bricks.Bricks)
            {
                if (brick == oldIfLogicBeginBrickReference.IfLogicEndBrick)
                {
                    newIfLogicBeginBrickReference.IfLogicEndBrick = newSprite.Scripts.Scripts[scriptCount].Bricks.Bricks[count] as IfLogicEndBrick;
                    return;
                }
                count++;
            }
        }

        public static void CleanUpReferencesAfterDelete(DataObject deletedObject, Sprite selectedSprite)
        {
            if(deletedObject is Costume)
                CleanUpCostumeReferences(deletedObject as Costume, selectedSprite);
            else if(deletedObject is Sound)
                CleanUpSoundReferences(deletedObject as Sound, selectedSprite);
            else if(deletedObject is Sprite)
                CleanUpSpriteReferences(deletedObject as Sprite);
            else if (deletedObject is UserVariable)
                CleanUpVariableReferences(deletedObject as UserVariable, selectedSprite);
        }

        private static void CleanUpCostumeReferences(Costume deletedCostume, Sprite selectedSprite)
        {
            foreach (Script script in selectedSprite.Scripts.Scripts)
            {
                foreach (Brick brick in script.Bricks.Bricks)
                {
                    if (brick is SetCostumeBrick)
                    {
                        var setCostumeBrick = brick as SetCostumeBrick;
                        if(setCostumeBrick.Costume == deletedCostume)
                            setCostumeBrick.CostumeReference = null;
                    }
                }
            }
        }

        private static void CleanUpSoundReferences(Sound deletedSound, Sprite selectedSprite)
        {
            foreach (Script script in selectedSprite.Scripts.Scripts)
            {
                foreach (Brick brick in script.Bricks.Bricks)
                {
                    if (brick is PlaySoundBrick)
                    {
                        var playSoundBrick = brick as PlaySoundBrick;
                        if(playSoundBrick.Sound == deletedSound)
                            playSoundBrick.SoundReference = null;
                    }
                }
            }
        }

        private static void CleanUpSpriteReferences(Sprite deletedSprite)
        {
            foreach (Sprite sprite in XmlParserTempProjectHelper.Project.SpriteList.Sprites)
            {
                foreach (Script script in sprite.Scripts.Scripts)
                {
                    foreach (Brick brick in script.Bricks.Bricks)
                    {
                        if (brick is PointToBrick)
                        {
                            var pointToBrick = brick as PointToBrick;
                            if(pointToBrick.PointedSprite == deletedSprite)
                                pointToBrick.PointedSpriteReference = null;
                        }
                    }
                }
            }
        }

        private static void CleanUpVariableReferences(UserVariable deletedUserVariable, Sprite selectedSprite)
        {
            foreach (Script script in selectedSprite.Scripts.Scripts)
            {
                foreach (Brick brick in script.Bricks.Bricks)
                {
                    if (brick is SetVariableBrick)
                    {
                        var setVariableBrick = brick as SetVariableBrick;
                        if(setVariableBrick.UserVariable == deletedUserVariable)
                            setVariableBrick.UserVariableReference = null;
                    }
                    else if (brick is ChangeVariableBrick)
                    {
                        var changeVariableBrick = brick as ChangeVariableBrick;
                        if(changeVariableBrick.UserVariable == deletedUserVariable)
                            changeVariableBrick.UserVariableReference = null;
                    }
                }
            }
        }
    }
}
