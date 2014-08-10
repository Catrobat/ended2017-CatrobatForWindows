using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    partial class XmlComeToFrontBrick
    {
        protected override Brick ToModel2(Context context)
        {
            return new BringToFrontBrick();
        }
    }
}