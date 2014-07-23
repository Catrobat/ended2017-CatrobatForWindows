using Catrobat.Data.Xml.XmlObjects.Bricks;
using Catrobat.Data.Xml.XmlObjects.Bricks.ControlFlow;
using Catrobat.Data.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Core.ExtensionMethods;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class BlockBrick
    {
    }

    partial class BlockBeginBrick
    {
    }

    partial class BlockEndBrick
    {
    }

    partial class BlockBeginBrick<TBegin, TEnd> 
    {
    }

    partial class BlockEndBrick<TBegin, TEnd>
    {
    }

    #region Implementations

    partial class ForeverBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            var result =  new XmlForeverBrick();
            context.Bricks[this] = result;
            result.LoopEndBrick = End == null ? null : (XmlForeverLoopEndBrick) End.ToXmlObject(context);
            return result;
        }
    }

    partial class EndForeverBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            var result = new XmlForeverLoopEndBrick();
            context.Bricks[this] = result;
            result.LoopBeginBrick = Begin == null ? null : (XmlForeverBrick) Begin.ToXmlObject(context);
            return result;
        }
    }

    partial class RepeatBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            var result = new XmlRepeatBrick
            {
                TimesToRepeat = Count == null ? new XmlFormula() : Count.ToXmlObject()
            };
            context.Bricks[this] = result;
            result.LoopEndBrick = End == null ? null : (XmlRepeatLoopEndBrick) End.ToXmlObject(context);
            return result;
        }
    }

    partial class EndRepeatBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            var result = new XmlRepeatLoopEndBrick();
            context.Bricks[this] = result;
            result.LoopBeginBrick = Begin == null ? null : (XmlRepeatBrick) Begin.ToXmlObject(context);
            return result;
        }
    }

    partial class IfBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            var result = new XmlIfLogicBeginBrick
            {
                IfCondition = Condition == null ? new XmlFormula() : Condition.ToXmlObject()
            };
            context.Bricks[this] = result;
            result.IfLogicElseBrick = Else == null ? null : (XmlIfLogicElseBrick) Else.ToXmlObject(context);
            result.IfLogicEndBrick = End == null ? null : (XmlIfLogicEndBrick) End.ToXmlObject(context);
            return result;
        }
    }

    partial class ElseBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            var result = new XmlIfLogicElseBrick();
            context.Bricks[this] = result;
            result.IfLogicBeginBrick = Begin == null ? null : (XmlIfLogicBeginBrick) Begin.ToXmlObject(context);
            result.IfLogicEndBrick = End == null ? null : (XmlIfLogicEndBrick) End.ToXmlObject(context);
            return result;
        }
    }

    partial class EndIfBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            var result = new XmlIfLogicEndBrick();
            context.Bricks[this] = result;
            result.IfLogicBeginBrick = Begin == null ? null : (XmlIfLogicBeginBrick) Begin.ToXmlObject(context);
            result.IfLogicElseBrick = Else == null ? null : (XmlIfLogicElseBrick) Else.ToXmlObject(context);
            return result;
        }
    }

    #endregion
}
