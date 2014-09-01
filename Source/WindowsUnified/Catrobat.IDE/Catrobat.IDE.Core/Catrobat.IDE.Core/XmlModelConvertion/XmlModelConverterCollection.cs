using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.XmlObjects;

namespace Catrobat.IDE.Core.XmlModelConvertion
{
    public class XmlModelConverterCollection<TConverterBase>
        where TConverterBase : IXmlModelConverter
    {
        // ReSharper disable once StaticFieldInGenericType
        private static Dictionary<Type, Dictionary<Type, IXmlModelConverter>> _xmlToModelConverterCache;
        // ReSharper disable once StaticFieldInGenericType
        private static Dictionary<Type, Dictionary<Type, IXmlModelConverter>> _modelToXmlConverterCache;

        private Dictionary<Type, IXmlModelConverter> _xmlToModelConverters;
        private Dictionary<Type, IXmlModelConverter> _modelToXmlConverters;

        public XmlModelConverterCollection()
        {
            if (_xmlToModelConverterCache == null)
                _xmlToModelConverterCache = new Dictionary<Type, Dictionary<Type, IXmlModelConverter>>();

            if (_modelToXmlConverterCache == null)
                _modelToXmlConverterCache = new Dictionary<Type, Dictionary<Type, IXmlModelConverter>>();

            FindAndRegisterConverters();
        }

        private void FindAndRegisterConverters()
        {
            var converterBaseType = typeof (TConverterBase);
            bool isInCache = true;

            if (_xmlToModelConverterCache.ContainsKey(converterBaseType))
                _xmlToModelConverters = _xmlToModelConverterCache[converterBaseType];
            else
                isInCache = false;

            if (_modelToXmlConverterCache.ContainsKey(converterBaseType))
                _modelToXmlConverters = _modelToXmlConverterCache[converterBaseType];
            else
                isInCache = false;

            if (isInCache) return;

            _xmlToModelConverters = new Dictionary<Type, IXmlModelConverter>();
            _modelToXmlConverters = new Dictionary<Type, IXmlModelConverter>();

            var currentAssembly = this.GetType().GetTypeInfo().Assembly;
            var typesInAssemblies = currentAssembly.DefinedTypes;

            var inAssemblies = typesInAssemblies as TypeInfo[] ?? typesInAssemblies.ToArray();

            var converterInstances = (from typeInfo in inAssemblies
                where typeInfo.ImplementedInterfaces.Contains(typeof(TConverterBase)) &&
                typeInfo.IsAbstract == false && typeInfo.ContainsGenericParameters == false
                                      select (TConverterBase)Activator.CreateInstance(typeInfo.AsType(), this)).ToList();

            foreach (var converter in converterInstances)
                RegisterConverter(converter);

            UpdateCache(typeof(TConverterBase));
        }

        private void RegisterConverter(TConverterBase converter)
        {
            var converterType = converter.GetType().GetTypeInfo().BaseType;
            var typeParameters = converterType.GetTypeInfo().GenericTypeArguments;
            var xmlType = typeParameters[0];
            var modelType = typeParameters[1];

            if (!_xmlToModelConverters.ContainsKey(xmlType))
                _xmlToModelConverters[xmlType] = converter;
            else
                throw new ArgumentException("The converter " + xmlType.Name + 
                    " is already registered.");

            if (!_modelToXmlConverters.ContainsKey(xmlType))
                _modelToXmlConverters[modelType] = converter;
            else
                throw new ArgumentException("The converter " + modelType.Name +
                    " is already registered.");
        }

        protected void UpdateCache(Type baseType)
        {
            if (_xmlToModelConverterCache == null)
                _xmlToModelConverterCache = new Dictionary<Type, Dictionary<Type, IXmlModelConverter>>();

            if (_modelToXmlConverterCache == null)
                _modelToXmlConverterCache = new Dictionary<Type, Dictionary<Type, IXmlModelConverter>>();

            _xmlToModelConverterCache.Add(baseType, _xmlToModelConverters);
            _modelToXmlConverterCache.Add(baseType, _modelToXmlConverters);
        }

        public ModelBase Convert(XmlObject o, XmlModelConvertContext c)
        {
            var xmlType = o.GetType();
            var converter = _xmlToModelConverters[xmlType];

            return _xmlToModelConverters[o.GetType()].Convert(o, c);
        }

        public XmlObject Convert(ModelBase m, XmlModelConvertBackContext c)
        {
            return _modelToXmlConverters[m.GetType()].Convert(m, c);
        }
    }
}
