//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Catrobat.IDE.Core.Models;
//using Catrobat.IDE.Core.Services;
//using Catrobat.IDE.Core.Xml.XmlObjects;
//using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

//namespace Catrobat.IDE.Core.XmlModelConvertion.Converters
//{
//    public class GlobalVariableConverter : XmlModelConverter<XmlUserVariable, GlobalVariable>
//    {
//        public GlobalVariableConverter() { }

//        public override GlobalVariable Convert(XmlUserVariable o, XmlModelConvertContext c)
//        {
//            return new GlobalVariable
//            {
//                Name = o.Name
//            };
//        }

//        public override XmlUserVariable Convert(GlobalVariable m, XmlModelConvertBackContext c)
//        {
//            return new XmlUserVariable
//            {
//                Name = m.Name
//            };
//        }
//    }
//}
