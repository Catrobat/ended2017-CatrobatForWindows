using System;
using System.Collections.Generic;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Scripts
{
    public class TappedScriptConverter : ScriptConverterBase<XmlWhenScript, TappedScript>
    {
        public TappedScriptConverter() { }

        public override TappedScript Convert1(XmlWhenScript o, XmlModelConvertContext c)
        {
            return new TappedScript();
        }

        public override XmlWhenScript Convert1(TappedScript m, XmlModelConvertBackContext c)
        {
            return new XmlWhenScript
            {
                Action = XmlWhenScript.WhenScriptAction.Tapped
            };
        }
    }
}
