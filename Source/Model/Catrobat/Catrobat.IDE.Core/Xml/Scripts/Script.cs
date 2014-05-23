using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.Bricks;
using Catrobat.IDE.Core.Xml.Scripts;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Scripts
{
    public abstract partial class Script : IXmlConvertible<XmlScript>
    {
        #region Implements IXmlConvertible

        public XmlScript ToXml()
        {
            var result = ToXml2();
            result.Bricks = new XmlBrickList
            {
                Bricks = Bricks.Select(brick => brick.ToXml()).ToObservableCollection()
            };
            return result;
        }

        protected abstract XmlScript ToXml2();
    
        #endregion
    }
}
