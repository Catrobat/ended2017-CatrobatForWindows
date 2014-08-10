using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt
{
    partial class XmlNxtPlayToneBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new PlayNxtToneBrick
            {
                Frequency = Frequency == null ? null : Frequency.ToModel(context), 
                Duration = DurationInSeconds == null ? null : DurationInSeconds.ToModel(context)
            };
        }
    }
}