using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    public class XmlModelConverterService
    {
        private readonly Dictionary<Type, IXmlModelConverter> _xmlToModelConverters;
        private readonly Dictionary<Type, IXmlModelConverter> _modelToXmlConverters;

        public XmlModelConverterService()
        {
            _xmlToModelConverters = new Dictionary<Type, IXmlModelConverter>();
            _modelToXmlConverters = new Dictionary<Type, IXmlModelConverter>();
        }

        public void RegisterConverter(IXmlModelConverter converter)
        {
            var converterType = converter.GetType();
            var typeParameters = converterType.GetTypeInfo().GenericTypeArguments;
            var xmlType = typeParameters[0];
            var modelType = typeParameters[1];

            if (_xmlToModelConverters.ContainsKey(xmlType))
                _xmlToModelConverters[xmlType] = converter;
            else
                throw new ArgumentException("The converter " + xmlType.Name + 
                    " is already registered.");

            if (_xmlToModelConverters.ContainsKey(xmlType))
                _modelToXmlConverters[modelType] = converter;
            else
                throw new ArgumentException("The converter " + modelType.Name +
                    " is already registered.");
        }

        public Model Convert(XmlObject o)
        {
            var xmlType = o.GetType();
            var converter = _xmlToModelConverters[xmlType];
            

            return _xmlToModelConverters[o.GetType()].Convert(o);
        }

        public XmlObject Convert(Model m)
        {
            return _modelToXmlConverters[m.GetType()].Convert(m);
        }
    }
}
