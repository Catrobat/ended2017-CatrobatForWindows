using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Costumes
{
    public partial class XmlSetCostumeBrick
    {
        internal protected override Brick ToModel2(Context context)
        {
            Costume costume = null;
            if (Costume != null) context.Costumes.TryGetValue(Costume, out costume);
            return new SetCostumeBrick
            {
                Value = costume
            };
        }
    }
}