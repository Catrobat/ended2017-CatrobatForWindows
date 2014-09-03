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
    public class VariableConverter<TVariable> : XmlModelConverter<XmlUserVariable, TVariable> where TVariable : Variable
    {
        public override TVariable Convert(XmlUserVariable o, XmlModelConvertContext c)
        {
            var variable = (TVariable)Activator.CreateInstance(typeof(TVariable));
            variable.Name = o.Name;
            return variable;
        }

        public override XmlUserVariable Convert(TVariable m, XmlModelConvertBackContext c)
        {
            return new XmlUserVariable
            {
                Name = m.Name
            };
        }
    }
}
