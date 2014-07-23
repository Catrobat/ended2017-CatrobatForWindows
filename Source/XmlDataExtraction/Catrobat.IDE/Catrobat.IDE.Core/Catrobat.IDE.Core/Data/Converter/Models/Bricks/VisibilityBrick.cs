using Catrobat.Data.Xml.XmlObjects.Bricks;
using Catrobat.Data.Xml.XmlObjects.Bricks.Properties;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class VisibilityBrick
    {
    }

    partial class ShowBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlShowBrick();
        }
    }

    partial class HideBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlHideBrick();
        }
    }
}
