using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Xml.Converter;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks
{
    partial class XmlBrick : IModelConvertible<Brick, Context>
    {
        Brick IModelConvertible<Brick, Context>.ToModel(Context context)
        {
            // prevents endless loops
            Brick result;
            if (context.Bricks.TryGetValue(this, out result)) return result;

            result = ToModel2(context);
            context.Bricks[this] = result;
            return result;
        }

        protected abstract Brick ToModel2(Context context);
    }
}
