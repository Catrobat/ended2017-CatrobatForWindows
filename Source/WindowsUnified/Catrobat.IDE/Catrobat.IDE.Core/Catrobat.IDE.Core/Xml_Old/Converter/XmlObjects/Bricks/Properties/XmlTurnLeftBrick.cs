using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlTurnLeftBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new TurnLeftBrick
            {
                RelativeValue = Degrees == null ? null : Degrees.ToModel(context)
            };
        }
    }
}