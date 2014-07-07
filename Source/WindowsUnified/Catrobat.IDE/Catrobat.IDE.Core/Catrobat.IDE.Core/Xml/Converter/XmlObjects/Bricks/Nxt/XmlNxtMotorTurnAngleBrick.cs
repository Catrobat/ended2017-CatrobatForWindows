using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Nxt
{
    partial class XmlNxtMotorTurnAngleBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new ChangeNxtMotorAngleBrick
            {
                Motor = Motor, 
                RelativeValue = Degrees == null ? null : Degrees.ToModel(context)
            };
        }
    }
}