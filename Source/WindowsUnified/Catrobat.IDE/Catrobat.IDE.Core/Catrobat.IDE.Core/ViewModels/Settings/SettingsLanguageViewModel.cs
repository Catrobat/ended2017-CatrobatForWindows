using System;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using System.Collections.ObjectModel;
using System.Globalization;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

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
                    return;

                ServiceLocator.CultureService.SetCulture(value);
                RaisePropertyChanged(() => CurrentCulture);
            }
        }

        #endregion

        #region Commands

        public RelayCommand<CultureInfo> SelectCultureCommand { get; set; }

        #endregion

        #region Actions
        private void SelectCultureAction(CultureInfo culture)
        {
            CurrentCulture = culture;

            var message = new MessageBase();
            Messenger.Default.Send(message, ViewModelMessagingToken.ClearPageCache);

            //ServiceLocator.NavigationService.NavigateTo(GetType());
            //ServiceLocator.NavigationService.RemoveBackEntry();
        }
        #endregion

        #region MessageActions


        #endregion

        public SettingsLanguageViewModel()
        {
            SelectCultureCommand = new RelayCommand<CultureInfo>(SelectCultureAction);
        }
    }
}