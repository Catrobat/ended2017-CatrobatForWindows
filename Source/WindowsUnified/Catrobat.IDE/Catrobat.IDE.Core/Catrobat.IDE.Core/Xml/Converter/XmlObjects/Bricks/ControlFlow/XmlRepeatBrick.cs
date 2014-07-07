using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.ExtensionMethods;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.ControlFlow
{
    partial class XmlRepeatBrick
    {
        protected override Brick ToModel2(Context context)
        {
            var result = new RepeatBrick
            {
                Count = TimesToRepeat == null ? null : TimesToRepeat.ToModel(context)
            };
            context.Bricks[this] = result;
            result.End = LoopEndBrick == null ? null : (EndRepeatBrick) LoopEndBrick.ToModel(context);
            return result;
        }
    }
}