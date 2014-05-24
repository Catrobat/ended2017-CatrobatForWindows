using Catrobat.IDE.Core.Xml.Scripts;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Scripts
{
    public partial class StartScript
    {
        protected override XmlScript ToXml2()
        {
            return new XmlStartScript();
        }
    }
}
