using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.Bricks;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Bricks
{
    public abstract partial class Brick : IXmlConvertible<XmlBrick>
    {
        #region Implements IXmlConvertible

        public abstract XmlBrick ToXml();

        #endregion
    }
}
