using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.ExtensionMethods;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    partial class XmlIfLogicBeginBrick
    {
        protected internal override Brick ToModel2(Context context)
        {
            var result = new IfBrick
            {
                Condition = IfCondition == null ? null : IfCondition.ToModel(context)
            };
            context.Bricks[this] = result;
            result.Else = (ElseBrick) IfLogicElseBrick.ToModel(context);
            result.End = (EndIfBrick) IfLogicEndBrick.ToModel(context);
            return result;
        }
    }
}