using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Context = Catrobat.IDE.Core.Xml.Converter.XmlProgramConverter.ConvertBackContext;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Scripts
{
    partial class EventScript
    {
        protected internal override XmlScript ToXmlObject2(Context context)
        {
            return new XmlWhenScript
            {
                Action = ToXmlObject3()
            };
        }

        protected internal abstract XmlWhenScript.WhenScriptAction ToXmlObject3();
    }

    #region Implementations

    partial class TappedScript
    {
        protected internal override XmlWhenScript.WhenScriptAction ToXmlObject3()
        {
            return XmlWhenScript.WhenScriptAction.Tapped;
        }
    }

    #endregion
}
