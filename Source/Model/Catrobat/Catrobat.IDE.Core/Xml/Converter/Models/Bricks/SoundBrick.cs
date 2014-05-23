using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class SoundBrick
    {
    }

    #region Implementations

    partial class PlaySoundBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlPlaySoundBrick
            {
                Sound = Value.ToXmlObject()
            };
        }
    }

    #endregion
}
