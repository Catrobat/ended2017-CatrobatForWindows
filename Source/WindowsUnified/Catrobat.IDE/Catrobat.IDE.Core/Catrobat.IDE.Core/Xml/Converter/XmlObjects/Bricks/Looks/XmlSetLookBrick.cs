using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Looks
{
    public partial class XmlSetLookBrick
    {
        protected override Brick ToModel2(Context context)
        {
            Look look = null;
            if (Look != null) context.Looks.TryGetValue(Look, out look);
            return new SetLookBrick
            {
                Value = look
            };
        }
    }
}