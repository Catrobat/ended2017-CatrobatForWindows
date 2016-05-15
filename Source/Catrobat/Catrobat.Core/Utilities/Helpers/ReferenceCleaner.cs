using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;

namespace Catrobat.IDE.Core.Utilities.Helpers
{
    public class ReferenceCleaner
    {
        public static void CleanUpLookReferences(Look deletedLook, Sprite selectedSprite)
        {
            foreach (var script in selectedSprite.Scripts)
            {
                foreach (var brick in script.Bricks)
                {
                    if (brick is SetLookBrick)
                    {
                        var setLookBrick = brick as SetLookBrick;
                        if (ReferenceEquals(setLookBrick.Value, deletedLook))
                            setLookBrick.Value = null;
                    }
                }
            }
        }

        public static void CleanUpSoundReferences(Sound deletedSound, Sprite selectedSprite)
        {
            foreach (var script in selectedSprite.Scripts)
            {
                foreach (var brick in script.Bricks)
                {
                    if (brick is PlaySoundBrick)
                    {
                        var playSoundBrick = brick as PlaySoundBrick;
                        if (ReferenceEquals(playSoundBrick.Value, deletedSound))
                            playSoundBrick.Value = null;
                    }
                }
            }
        }

        public static void CleanUpSpriteReferences(Sprite deletedSprite, Program currentProject)
        {
            foreach (var sprite in currentProject.Sprites)
            {
                foreach (var script in sprite.Scripts)
                {
                    foreach (var brick in script.Bricks)
                    {
                        if (brick is LookAtBrick)
                        {
                            var pointToBrick = brick as LookAtBrick;
                            if (ReferenceEquals(pointToBrick.Target, deletedSprite))
                                pointToBrick.Target = null;
                        }
                    }
                }
            }
        }

        public static void CleanUpVariableReferences(Variable deletedUserVariable, Sprite selectedSprite)
        {
            foreach (var script in selectedSprite.Scripts)
            {
                foreach (var brick in script.Bricks)
                {
                    if (brick is VariableBrick)
                    {
                        var setVariableBrick = brick as VariableBrick;
                        if (ReferenceEquals(setVariableBrick.Variable, deletedUserVariable))
                            setVariableBrick.Variable = null;
                    }
                }
            }
        }
    }
}
