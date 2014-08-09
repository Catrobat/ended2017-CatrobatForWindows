using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Sounds;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertBackContext;

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


    partial class SpeakBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlSpeakBrick
            {
                Text = Value
            };
        }
    }

    partial class StopSoundsBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlStopAllSoundsBrick();
        }
    }

    partial class SetVolumeBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlSetVolumeToBrick
            {
                Volume = Percentage == null ? null : Percentage.ToXmlObject()
            };
        }
    }

    partial class ChangeVolumeBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlChangeVolumeByBrick
            {
                Volume = RelativePercentage == null ? null : RelativePercentage.ToXmlObject()
            };
        }
    }

    #endregion
}
