//using System;
//using System.Collections.Generic;
//using Catrobat.IDE.Core.Models.Scripts;
//using Catrobat.IDE.Core.Services;
//using Catrobat.IDE.Core.Xml.XmlObjects.Bricks;
//using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;

//namespace Catrobat.IDE.Core.XmlModelConvertion.Converters.Actions.Scripts
//{
//    public class EventScriptConverter<TEventScript> : ScriptConverterBase<XmlWhenScript, TEventScript>
//        where TEventScript : EventScript
//    {
//        public EventScriptConverter() { }

//        public override TEventScript Convert1(XmlWhenScript o, XmlModelConvertContext c)
//        {
//            //switch(o.Action)
//            var variable = (TEventScript)Activator.CreateInstance(typeof(TEventScript));
//            return variable;
//        }

//        public override XmlWhenScript Convert1(TEventScript m, XmlModelConvertBackContext c)
//        {
//            if (m is TappedScript) 
//            {
//                return new XmlWhenScript
//                {
//                    Action = new XmlWhenScript.WhenScriptAction()
//                };
//            }
//            throw new ArgumentException("Type of WhenScript not known.");
//        }
//    }
//}
