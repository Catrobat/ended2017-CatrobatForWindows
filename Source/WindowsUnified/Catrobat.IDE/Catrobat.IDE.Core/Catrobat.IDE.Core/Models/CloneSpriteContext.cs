using System.Collections.Generic;
using Catrobat.IDE.Core.Models.Bricks;

namespace Catrobat.IDE.Core.Models
{
    public class CloneSpriteContext
    {
        #region Properties

        private readonly IReadOnlyDictionary<Look, Look> _looks;
        public IReadOnlyDictionary<Look, Look> Looks
        {
            get { return _looks; }
        }

        private readonly IReadOnlyDictionary<Sound, Sound> _sounds;
        public IReadOnlyDictionary<Sound, Sound> Sounds
        {
            get { return _sounds; }
        }

        private readonly IReadOnlyDictionary<LocalVariable, LocalVariable> _localVariables;
        public IReadOnlyDictionary<LocalVariable, LocalVariable> LocalVariables
        {
            get { return _localVariables; }
        }

        private readonly IDictionary<Brick, Brick> _bricks;
        public IDictionary<Brick, Brick> Bricks
        {
            get { return _bricks; }
        }

        #endregion

        public CloneSpriteContext(
            IReadOnlyDictionary<Look, Look> looks,
            IReadOnlyDictionary<Sound, Sound> sounds, 
            IReadOnlyDictionary<LocalVariable, LocalVariable> localVariables)
        {
            _looks = looks;
            _sounds = sounds;
            _localVariables = localVariables;
            _bricks = new Dictionary<Brick, Brick>();
        }
    }
}
