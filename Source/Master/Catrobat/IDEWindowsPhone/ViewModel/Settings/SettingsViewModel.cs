using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.Helpers;
using Catrobat.IDEWindowsPhone.Content.Localization;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.Views.Settings;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using IDEWindowsPhone;

namespace Catrobat.IDEWindowsPhone.ViewModel.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Private Members

        private readonly ThemeChooser _themeChooser = (App.Current.Resources["ThemeChooser"] as ThemeChooser);
        private readonly MemoryMonitor _memoryMonitor;

        #endregion

        #region Properties

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

        public Theme ActiveTheme
        {
            get { return _themeChooser.SelectedTheme; }
            set { _themeChooser.SelectedTheme = value; }
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

                ((LocalizedStrings) Application.Current.Resources["LocalizedStrings"]).Reset();
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
            Navigation.NavigateTo(typeof (SettingsThemeView));
        }

        private void ShowBrickSettingsAction()
        {
            Navigation.NavigateTo(typeof (SettingsBrickView));
        }

        private void ShowLanguageSettingsAction()
        {
            Navigation.NavigateTo(typeof (SettingsLanguageView));
        }

        private void ActiveThemeChangedAction(Theme newTheme)
        {
            ActiveTheme = newTheme;
            Navigation.NavigateBack();
        }

        #endregion

        public SettingsViewModel()
        {
            ShowDesignSettingsCommand = new RelayCommand(ShowDesignSettingsAction);
            ShowBrickSettingsCommand = new RelayCommand(ShowBrickSettingsAction);
            ShowLanguageSettingsCommand = new RelayCommand(ShowLanguageSettingsAction);
            ActiveThemeChangedCommand = new RelayCommand<Theme>(ActiveThemeChangedAction);

            _themeChooser.PropertyChanged += ThemeChooserOnPropertyChanged;
            _memoryMonitor = Debugger.IsAttached ? new MemoryMonitor(true, false) : new MemoryMonitor(false, false);
        }

        private void ThemeChooserOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == PropertyNameHelper.GetPropertyNameFromExpression(() => _themeChooser.SelectedTheme))
            {
                RaisePropertyChanged(() => ActiveTheme);
            }
        }
    }
}