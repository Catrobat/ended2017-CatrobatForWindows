using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;

namespace Catrobat.IDE.Core.Models
{
    /// <summary>
    /// Bad hack because <see cref="Script"/> and <see cref="Brick"/> are semantically both bricks (see Task 426)
    /// </summary>
    public interface IBrick : ICloneable, ICloneable<CloneSpriteContext>
    {
        bool IsAttached { get; set; }
    }
}
