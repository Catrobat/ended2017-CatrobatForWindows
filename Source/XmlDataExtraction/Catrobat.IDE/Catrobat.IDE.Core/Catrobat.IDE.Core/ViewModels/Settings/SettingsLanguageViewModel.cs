using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Catrobat.IDE.Core.ViewModels.Settings
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
            get { return ServiceLocator.CultureService.GetCulture(); }

            set
            {
                if (ServiceLocator.CultureService.GetCulture().Equals(value))
                {
                    return;
                }

                ServiceLocator.CultureService.SetCulture(value);
                RaisePropertyChanged(() => CurrentCulture);

                ServiceLocator.NavigationService.RemoveBackEntry();
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