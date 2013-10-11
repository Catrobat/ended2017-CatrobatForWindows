using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Phone.Content.Localization;
using Catrobat.IDE.Phone.Themes;
using Catrobat.IDE.Phone.Views.Settings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Phone.ViewModel.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Private Members

        private ThemeChooser _themeChooser;

        #endregion

        #region Properties

        public ThemeChooser ThemeChooser
        {
            get { return _themeChooser; }
            set { _themeChooser = value; RaisePropertyChanged(() => ThemeChooser); }
        }

        public bool ShowMemoryMonitorOption
        {
            get { return Debugger.IsAttached; }
        }

        public ObservableCollection<Theme> AvailableThemes
        {
            get { return _themeChooser.Themes; }
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

        public ObservableCollection<CultureInfo> AvailableCultures
        {
            get { return LanguageHelper.SupportedLanguages; }
        }

        #endregion

        #region Commands

        public RelayCommand ShowDesignSettingsCommand { get; private set; }

        public RelayCommand ShowBrickSettingsCommand { get; private set; }

        public RelayCommand ShowLanguageSettingsCommand { get; private set; }

        public ICommand ActiveThemeChangedCommand { get; private set; }

        #endregion

        #region Actions

        private void ShowDesignSettingsAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(SettingsThemeView));
        }

        private void ShowBrickSettingsAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(SettingsBrickView));
        }

        private void ShowLanguageSettingsAction()
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(SettingsLanguageView));
        }

        private void ActiveThemeChangedAction(Theme newTheme)
        {
            ThemeChooser.SelectedTheme = newTheme;
            ServiceLocator.NavigationService.NavigateBack();
        }

        #endregion

        #region MessageActions

        private void ThemeChooserInitializedMessageAction(GenericMessage<ThemeChooser> message)
        {
            ThemeChooser = message.Content;
        }

        #endregion

        public SettingsViewModel()
        {
            ShowDesignSettingsCommand = new RelayCommand(ShowDesignSettingsAction);
            ShowBrickSettingsCommand = new RelayCommand(ShowBrickSettingsAction);
            ShowLanguageSettingsCommand = new RelayCommand(ShowLanguageSettingsAction);
            ActiveThemeChangedCommand = new RelayCommand<Theme>(ActiveThemeChangedAction);

            Messenger.Default.Register<GenericMessage<ThemeChooser>>(this,
                 ViewModelMessagingToken.ThemeChooserListener, ThemeChooserInitializedMessageAction);
        }
    }
}