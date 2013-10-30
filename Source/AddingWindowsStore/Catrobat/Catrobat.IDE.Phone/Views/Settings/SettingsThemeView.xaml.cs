using System.ComponentModel;
using System.Windows;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Settings;
using Microsoft.Phone.Controls;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Catrobat.IDE.Phone.Views.Settings
{
    public partial class SettingsThemeView : PhoneApplicationPage
    {
        private readonly SettingsThemeViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).SettingsThemeViewModel;

        public SettingsThemeView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }

        private void Item_Tap(object sender, GestureEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement != null)
            {
                var theme = frameworkElement.DataContext as Theme;
                _viewModel.ActiveThemeChangedCommand.Execute(theme);
            }
        }
    }
}