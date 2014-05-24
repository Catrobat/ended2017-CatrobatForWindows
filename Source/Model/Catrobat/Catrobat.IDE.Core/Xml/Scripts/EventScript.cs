using Catrobat.IDE.Core.Xml.Scripts;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Scripts
{
    public abstract partial class EventScript
    {
        #region Implements IXmlConvertible

        protected override XmlScript ToXml2()
        {
            return new XmlWhenScript
            {
                Action = ToXml3()
            };
        }

        protected abstract XmlWhenScript.WhenScriptAction ToXml3();

        #endregion
    }

    #region Implementations

    public partial class TappedScript
    {
        protected override XmlWhenScript.WhenScriptAction ToXml3()
        {
            return XmlWhenScript.WhenScriptAction.Tapped;
        }
    }

    #endregion
}
