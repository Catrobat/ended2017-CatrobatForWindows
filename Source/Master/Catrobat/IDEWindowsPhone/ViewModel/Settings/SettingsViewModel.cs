using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Catrobat.Core.Misc.Helpers;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.Views.Settings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone.ViewModel.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Private Members

        private ThemeChooser _themeChooser;
        private readonly MemoryMonitor _memoryMonitor;

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

        public bool ShowMemoryMonitor
        {
            get { return _memoryMonitor.ShowVisualization; }
            set
            {
                if (_memoryMonitor.ShowVisualization == value)
                {
                    return;
                }

                _memoryMonitor.ShowVisualization = value;
                RaisePropertyChanged(() => ShowMemoryMonitor);
            }
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
            Navigation.NavigateTo(typeof(SettingsThemeView));
        }

        private void ShowBrickSettingsAction()
        {
            Navigation.NavigateTo(typeof(SettingsBrickView));
        }

        private void ShowLanguageSettingsAction()
        {
            Navigation.NavigateTo(typeof(SettingsLanguageView));
        }

        private void ActiveThemeChangedAction(Theme newTheme)
        {
            ThemeChooser.SelectedTheme = newTheme;
            Navigation.NavigateBack();
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

            _memoryMonitor = Debugger.IsAttached ? new MemoryMonitor(true, false) : new MemoryMonitor(false, false);

            Messenger.Default.Register<GenericMessage<ThemeChooser>>(this,
                 ViewModelMessagingToken.ThemeChooserListener, ThemeChooserInitializedMessageAction);
        }
    }
}