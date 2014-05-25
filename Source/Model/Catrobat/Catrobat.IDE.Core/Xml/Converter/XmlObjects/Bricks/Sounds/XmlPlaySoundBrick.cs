using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds
{
    partial class XmlPlaySoundBrick
    {
        protected override Brick ToModel2(Context context)
        {
            Sound sound = null;
            if (Sound != null) context.Sounds.TryGetValue(Sound, out sound);
            return new PlaySoundBrick
            {
                Value = sound
            };
        }
    }
}