using System;
using System.ComponentModel;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core
{
    public abstract class CatrobatContextBase : INotifyPropertyChanged
    {

        #region Properties

        public LocalSettings LocalSettings { get; set; }

        public string CurrentToken
        {
            get { return LocalSettings.CurrentToken; }

            set
            {
                if (LocalSettings.CurrentToken == value) return;

                LocalSettings.CurrentToken = value;
                RaisePropertyChanged();
            }
        }

        public string CurrentUserEmail
        {
            get { return LocalSettings.CurrentUserEmail; }

            set
            {
                if (LocalSettings.CurrentUserEmail == value) return;

                LocalSettings.CurrentUserEmail = value;
                RaisePropertyChanged();
            }
        }

        public string CurrentUserName
        {
            get { return LocalSettings.CurrentUsername; }

            set
            {
                if (LocalSettings.CurrentUsername == value) return;

                LocalSettings.CurrentUsername = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]

        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

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