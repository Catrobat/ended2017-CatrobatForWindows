using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.ExtensionMethods;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    partial class XmlIfLogicElseBrick
    {
        protected override Brick ToModel2(Context context)
        {
            var result = new ElseBrick();
            context.Bricks[this] = result;
            result.Begin = IfLogicBeginBrick == null ? null : (IfBrick) IfLogicBeginBrick.ToModel(context);
            result.End = IfLogicEndBrick == null ? null : (EndIfBrick) IfLogicEndBrick.ToModel(context);
            return result;
        }
    }
}