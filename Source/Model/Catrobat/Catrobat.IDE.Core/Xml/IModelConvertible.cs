using System.ComponentModel;

namespace Catrobat.IDE.Core.Xml
{
    public interface IModelConvertible<out TModel> where TModel : INotifyPropertyChanged
    {
        TModel ToModel();
    }

    public interface IModelConvertible<out TModel, in TContext> where TModel : INotifyPropertyChanged
    {
        TModel ToModel(TContext context);
    }
}
