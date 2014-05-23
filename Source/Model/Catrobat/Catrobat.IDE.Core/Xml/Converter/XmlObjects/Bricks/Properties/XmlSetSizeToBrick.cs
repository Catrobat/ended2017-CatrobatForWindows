using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.ExtensionMethods;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlSetSizeToBrick
    {
        protected internal override Brick ToModel2(XmlProjectConverter.ConvertContext context)
        {
            return new SetSizeBrick
            {
                Percentage = Size == null ? null : Size.ToModel(context)
            };
        }
    }
}