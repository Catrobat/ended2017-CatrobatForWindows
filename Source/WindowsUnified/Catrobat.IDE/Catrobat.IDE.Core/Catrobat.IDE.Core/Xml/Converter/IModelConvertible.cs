using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Xml.Converter
{
    public interface IModelConvertible
    {
    }

    public interface IModelConvertible<out TModel> : IModelConvertible where TModel : Model
    {
        TModel ToModel();
    }

    public interface IModelConvertible<out TModel, in TContext> : IModelConvertible where TModel : Model
    {
        TModel ToModel(TContext context);
    }

    public interface IModelConvertibleCyclic<out TModel, in TContext> : IModelConvertible where TModel : Model
    {
        TModel ToModel(TContext context, bool pointerOnly = false);
    }
}
