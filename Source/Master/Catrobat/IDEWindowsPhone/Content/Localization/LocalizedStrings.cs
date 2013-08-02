using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Catrobat.Core.Misc;
using Catrobat.IDECommon.Resources.IDE.Main;

namespace Catrobat.IDEWindowsPhone.Content.Localization
{
  public class LocalizedStrings : INotifyPropertyChanged
  {
      private static readonly AppResources ResourcesField = new AppResources();

      public AppResources Resources
      {
          get
          {
              return ResourcesField;
          }
      }


      public void Reset()
    {
      RaisePropertyChanged(() => Resources);
    }

    #region INotifyPropertyChanged region
    public event PropertyChangedEventHandler PropertyChanged;

    public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
    {
      if (PropertyChanged != null)
      {
          PropertyChanged(this, new PropertyChangedEventArgs(PropertyNameHelper.GetPropertyNameFromExpression(selector)));
      }
    }
    #endregion
  }
}
