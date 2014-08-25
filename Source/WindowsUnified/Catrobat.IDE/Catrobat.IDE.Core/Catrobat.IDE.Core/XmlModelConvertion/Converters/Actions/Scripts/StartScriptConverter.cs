using System;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Scripts
{
    public class StartScriptConverter : ScriptConverterBase<XmlStartScript, StartScript>
    {
        public override StartScript Convert1(XmlStartScript o, XmlModelConvertContext c)
        {
            return new StartScript();
        }

        public override XmlStartScript Convert1(StartScript m, XmlModelConvertBackContext c)
        {
            return new XmlStartScript();
        }
    }
}
