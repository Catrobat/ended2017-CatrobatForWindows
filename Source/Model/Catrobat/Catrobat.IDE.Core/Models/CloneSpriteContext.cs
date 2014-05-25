using System.Collections.Generic;
using Catrobat.IDE.Core.Models.Bricks;

namespace Catrobat.IDE.Core.Models
{
    public class CloneSpriteContext
    {
        #region Properties

        private readonly IReadOnlyDictionary<Costume, Costume> _costumes;
        public IReadOnlyDictionary<Costume, Costume> Costumes
        {
            get { return _costumes; }
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
            IReadOnlyDictionary<Costume, Costume> costumes,
            IReadOnlyDictionary<Sound, Sound> sounds, 
            IReadOnlyDictionary<LocalVariable, LocalVariable> localVariables)
        {
            _costumes = costumes;
            _sounds = sounds;
            _localVariables = localVariables;
            _bricks = new Dictionary<Brick, Brick>();
        }
    }
}
