using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core.XmlModelConvertion.Converters
{
    public class LocalVariableConverter : XmlModelConverter<XmlUserVariable, LocalVariable>
    {
        public LocalVariableConverter() { }

        public override LocalVariable Convert(XmlUserVariable o, XmlModelConvertContext c)
        {
            return new LocalVariable
            {
                Name = o.Name
            };
        }

        public override XmlUserVariable Convert(LocalVariable m, XmlModelConvertBackContext c)
        {
            return new XmlUserVariable
            {
                Name = m.Name
            };
        }
    }
}
