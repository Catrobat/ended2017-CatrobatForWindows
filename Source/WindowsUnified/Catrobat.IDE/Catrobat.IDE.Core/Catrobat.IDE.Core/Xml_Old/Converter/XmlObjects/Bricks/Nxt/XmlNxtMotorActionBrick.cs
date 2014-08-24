using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt
{
    partial class XmlNxtMotorActionBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new SetNxtMotorSpeedBrick
            {
                Motor = Motor,
                Percentage = Speed == null ? null : Speed.ToModel(context)
            };
        }
    }
}