using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.Core.Misc.Helpers
{
    public class ReferenceHelper
    {
        public static Project Project;

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
            if (referenceObject is LoopBeginBrickRef)
            {
                var loopBeginBrickReference = referenceObject as LoopBeginBrickRef;
                if (loopBeginBrickReference.Class == "forever")
                    return GetForeverBrickReferenceString(loopBeginBrickReference.LoopBeginBrick);
                if (loopBeginBrickReference.Class == "repeat")
                    return GetRepeatBrickReferenceString(loopBeginBrickReference.LoopBeginBrick);
            }
            if (referenceObject is LoopEndBrickRef)
                return GetLoopEndBrickReferenceString(referenceObject as LoopEndBrickRef);

            return "";
        }

        private static string GetCostumeReferenceString(Costume costume)
        {
            foreach (var sprite in Project.SpriteList.Sprites)
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
            foreach (var sprite in Project.SpriteList.Sprites)
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

            foreach (var tempSprite in Project.SpriteList.Sprites)
            {
                count++;
                if (tempSprite == sprite)
                    break;
            }

            foreach (var tempSprite in Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is PointToBrick)
                        {
                            var pointToBrick = brick as PointToBrick;
                            if (pointToBrick.PointedSpriteReference == spriteReference)
                                return "../../../../../../object[" + count + "]";
                        }
                    }

            foreach (var entry in Project.VariableList.ObjectVariableList.ObjectVariableEntries)
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
            foreach (var entry in Project.VariableList.ObjectVariableList.ObjectVariableEntries)
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
            foreach (var tempUserVariable in Project.VariableList.ProgramVariableList.UserVariables)
            {
                count++;
                if (tempUserVariable == userVariable)
                    return "../../../../../variables/programVariableList/userVariable[" + count + "]";
            }

            return "";
        }

        private static string GetForeverBrickReferenceString(LoopBeginBrick loopBeginBrick)
        {
            foreach (var sprite in Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    var count = 0;
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is ForeverBrick)
                        {
                            count++;
                            if (brick == loopBeginBrick)
                                return "../../brickList/foreverBrick[" + count + "]";
                        }
                }
            return "";
        }

        private static string GetRepeatBrickReferenceString(LoopBeginBrick loopBeginBrick)
        {
            foreach (var sprite in Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    var count = 0;
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is RepeatBrick)
                        {
                            count++;
                            if (brick == loopBeginBrick)
                                return "../../brickList/repeatBrick[" + count + "]";
                        }
                }
            return "";
        }

        private static string GetLoopEndBrickReferenceString(LoopEndBrickRef loopEndBrickRef)
        {
            var loopEndBrick = loopEndBrickRef.LoopEndBrick;

            foreach (var sprite in Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                {
                    var count = 0;
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is LoopEndBrick)
                        {
                            count++;
                            if (brick == loopEndBrick)
                                return "../../brickList/loopEndBrick[" + count + "]";
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
            if (dataObject is LoopBeginBrickRef)
            {
                var loopBeginBrickReference = dataObject as LoopBeginBrickRef;
                if (loopBeginBrickReference.Class == "forever")
                    return GetForeverBrickObject(loopBeginBrickReference, reference);
                if (loopBeginBrickReference.Class == "repeat")
                    return GetRepeatBrickObject(loopBeginBrickReference, reference);
            }
            if (dataObject is LoopEndBrickRef)
                return GetLoopEndBrickObject(dataObject as LoopEndBrickRef);


            return null;
        }

        private static DataObject GetCostumeObject(CostumeReference costumeReference, string reference)
        {
            foreach (var sprite in Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is SetCostumeBrick)
                        {
                            var setCostumeBrick = brick as SetCostumeBrick;
                            if (setCostumeBrick.CostumeReference == costumeReference)
                            {
                                var count = 0;
                                if (reference.Contains("["))
                                {
                                    reference = reference.Split('[')[1];
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
            foreach (var sprite in Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is PlaySoundBrick)
                        {
                            var playSoundBrick = brick as PlaySoundBrick;
                            if (playSoundBrick.SoundReference == soundReference)
                            {
                                var count = 0;
                                if (reference.Contains("["))
                                {
                                    reference = reference.Split('[')[1];
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
            if (reference.Contains("["))
            {
                reference = reference.Split('[')[1];
                reference = reference.Split(']')[0];
                count = Int32.Parse(reference) - 1;
            }
            return Project.SpriteList.Sprites[count];
        }

        private static DataObject GetUserVariableObject(UserVariableReference userVariableReference, string reference)
        {
            foreach (var sprite in Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is SetVariableBrick || brick is ChangeVariableBrick)
                        {
                            if ((brick as SetVariableBrick).UserVariableReference == userVariableReference ||
                                (brick as ChangeVariableBrick).UserVariableReference == userVariableReference)
                            {
                                var count = 0;
                                if (reference.Contains("["))
                                {
                                    reference = reference.Split('[')[1];
                                    reference = reference.Split(']')[0];
                                    count = Int32.Parse(reference) - 1;
                                }
                                return sprite.Costumes.Costumes[count];
                            }
                        }
                    }
            return null;
        }

        private static DataObject GetForeverBrickObject(LoopBeginBrickRef loopBeginBrickReference, string reference)
        {
            var foreverBricks = new List<Brick>();
            foreach (var sprite in Project.SpriteList.Sprites)
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
                                if (reference.Contains("["))
                                {
                                    reference = reference.Split('[')[1];
                                    reference = reference.Split(']')[0];
                                    count = Int32.Parse(reference) - 1;
                                }
                                return foreverBricks[count];
                            }
                        }
                    }
            return null;
        }

        private static DataObject GetRepeatBrickObject(LoopBeginBrickRef loopBeginBrickReference, string reference)
        {
            var repeatBrick = new List<Brick>();
            foreach (var sprite in Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                    {
                        if (brick is RepeatBrick)
                            repeatBrick.Add(brick as RepeatBrick);
                        if (brick is LoopEndBrick)
                        {
                            var loopEndBrick = brick as LoopEndBrick;
                            if (loopEndBrick.LoopBeginBrickReference == loopBeginBrickReference)
                            {
                                var count = 0;
                                if (reference.Contains("["))
                                {
                                    reference = reference.Split('[')[1];
                                    reference = reference.Split(']')[0];
                                    count = Int32.Parse(reference) - 1;
                                }
                                return repeatBrick[count];
                            }
                        }
                    }
            return null;
        }

        private static DataObject GetLoopEndBrickObject(LoopEndBrickRef loopEndBrickReference)
        {
            bool found = false;

            foreach (var sprite in Project.SpriteList.Sprites)
                foreach (var script in sprite.Scripts.Scripts)
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
                        if (found && brick is LoopEndBrick)
                            return brick;
                    }
            return null;
        }
    }
}
