using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.Core.Resources.Localization;

namespace Catrobat.IDE.Core.Resources.Localization
{
    public class LocalizedStrings : INotifyPropertyChanged
    {
        //private static readonly AppResources ResourcesField = new AppResources();

        //public AppResources Resources
        //{
        //    get
        //    {
        //        return ResourcesField;
        //    }
        //}

        #region INotifyPropertyChanged region
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyHelper.GetPropertyName(selector)));
            }
        }
        #endregion
    }
}
