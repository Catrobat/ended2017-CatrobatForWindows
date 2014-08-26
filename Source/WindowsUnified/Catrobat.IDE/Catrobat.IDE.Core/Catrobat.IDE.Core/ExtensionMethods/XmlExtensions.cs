using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Xml.Converter;

namespace Catrobat.IDE.Core.ExtensionMethods
{
    public static class XmlExtensions
    {
        internal static TXmlObject ToXmlObject<TXmlObject>(this IXmlObjectConvertible<TXmlObject> model)
        {
            return model.ToXmlObject();
        }

        internal static TXmlObject ToXmlObject<TXmlObject, TContext>(this IXmlObjectConvertible<TXmlObject, TContext> model, TContext context)
        {
            return model.ToXmlObject(context);
        }

        internal static TXmlObject ToXmlObject<TXmlObject, TContext>(this IXmlObjectConvertibleCyclic<TXmlObject, TContext> model, TContext context, bool pointerOnly = false)
        {
            return model.ToXmlObject(context, pointerOnly);
        }

        internal static TModel ToModel<TModel>(this IModelConvertible<TModel> xmlObject) where TModel  : ModelBase
        {
            return xmlObject.ToModel();
        }

        internal static TModel ToModel<TModel, TContext>(this IModelConvertible<TModel, TContext> xmlObject, TContext context) where TModel  : ModelBase
        {
            return xmlObject.ToModel(context);
        }

        internal static TModel ToModel<TModel, TContext>(this IModelConvertibleCyclic<TModel, TContext> xmlObject, TContext context, bool pointerOnly = false) where TModel  : ModelBase
        {
            return xmlObject.ToModel(context, pointerOnly);
        }
    }
}
