using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Resources.Localization;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Phone.ViewModel.Settings
{
    public class SettingsLanguageViewModel : ViewModelBase
    {
        #region Private Members

        #endregion

        #region Properties

        public ObservableCollection<CultureInfo> AvailableCultures
        {
            get { return LanguageHelper.SupportedLanguages; }
        }

        public CultureInfo CurrentCulture
        {
            get { return Thread.CurrentThread.CurrentCulture; }

            set
            {
                if (Thread.CurrentThread.CurrentCulture.Equals(value))
                {
                    return;
                }

                Thread.CurrentThread.CurrentCulture = value;
                Thread.CurrentThread.CurrentUICulture = value;

                ((LocalizedStrings)Application.Current.Resources["LocalizedStrings"]).Reset();
                RaisePropertyChanged(() => CurrentCulture);
            }
        }

        #endregion

        #region Commands

        #endregion

        #region Actions

       

        #endregion

        #region MessageActions


        #endregion

        public SettingsLanguageViewModel()
        {

        }
    }
}