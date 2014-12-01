using System.Collections.ObjectModel;
using System.Windows.Input;
using Catrobat.IDE.Core.UI;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Core.ViewModels.Settings
{
    public class SettingsThemeViewModel : ViewModelBase
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

        public ObservableCollection<Theme> AvailableThemes
        {
            get { return _themeChooser.Themes; }
        }

        #endregion

        #region Commands

        public ICommand ActiveThemeChangedCommand { get; private set; }

        #endregion

        #region Actions

        private void ActiveThemeChangedAction(Theme newTheme)
        {
            ThemeChooser.SelectedTheme = newTheme;
        }

        protected override void GoBackAction()
        {
            base.GoBackAction();
        }

        #endregion

        #region MessageActions

        private void ThemeChooserInitializedMessageAction(GenericMessage<ThemeChooser> message)
        {
            ThemeChooser = message.Content;
        }

        #endregion

        public SettingsThemeViewModel()
        {
            ActiveThemeChangedCommand = new RelayCommand<Theme>(ActiveThemeChangedAction);

            Messenger.Default.Register<GenericMessage<ThemeChooser>>(this,
                 ViewModelMessagingToken.ThemeChooserListener, ThemeChooserInitializedMessageAction);
        }
    }
}