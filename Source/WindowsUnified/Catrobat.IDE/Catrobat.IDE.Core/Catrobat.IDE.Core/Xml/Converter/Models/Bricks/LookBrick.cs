using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Looks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class LookBrick
    {
    }

    #region Implementations

    partial class SetLookBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            XmlLook look = null;
            if (Value != null) context.Looks.TryGetValue(Value, out look);
            return new XmlSetLookBrick
            {
                Look = look
            };
        }
    }

    partial class NextLookBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlNextLookBrick();
        }
    }

    #endregion
}
