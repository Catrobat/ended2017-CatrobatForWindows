using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Catrobat.IDECommon.Resources.Bricks;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDECommon.Resources.Main;

namespace Catrobat.IDECommon.Resources
{
  public class LocalizedStrings : INotifyPropertyChanged
  {
    private static readonly MainResources _main = new MainResources();
    public MainResources Main { get { return _main; } }

    private static readonly EditorResources _editor = new EditorResources();
    public EditorResources Editor { get { return _editor; } }

    private static readonly BrickResources _bricks = new BrickResources();
    public BrickResources Bricks { get { return _bricks; } }

    public void Reset()
    {
      RaisePropertyChanged(() => Main);
      RaisePropertyChanged(() => Editor);
      RaisePropertyChanged(() => Bricks);
    }

    #region INotifyPropertyChanged region
    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, new PropertyChangedEventArgs(GetPropertyNameFromExpression(selector)));
      }
    }

    public static string GetPropertyNameFromExpression<T>(Expression<Func<T>> property)
    {
      var lambda = (LambdaExpression)property;
      MemberExpression memberExpression;

      if (lambda.Body is UnaryExpression)
      {
        var unaryExpression = (UnaryExpression)lambda.Body;
        memberExpression = (MemberExpression)unaryExpression.Operand;
      }
      else
      {
        memberExpression = (MemberExpression)lambda.Body;
      }

      return memberExpression.Member.Name;
    }
    #endregion
  }
}
