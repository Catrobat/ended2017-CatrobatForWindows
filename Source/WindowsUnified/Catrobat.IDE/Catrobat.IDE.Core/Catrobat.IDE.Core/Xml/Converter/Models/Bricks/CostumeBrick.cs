using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Costumes;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProjectConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    partial class CostumeBrick
    {
    }

    #region Implementations

    partial class SetCostumeBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            XmlCostume costume = null;
            if (Value != null) context.Costumes.TryGetValue(Value, out costume);
            return new XmlSetCostumeBrick
            {
                Costume = costume
            };
        }
    }

    partial class NextCostumeBrick
    {
        protected internal override XmlBrick ToXmlObject2(Context context)
        {
            return new XmlNextCostumeBrick();
        }
    }

    #endregion
}
