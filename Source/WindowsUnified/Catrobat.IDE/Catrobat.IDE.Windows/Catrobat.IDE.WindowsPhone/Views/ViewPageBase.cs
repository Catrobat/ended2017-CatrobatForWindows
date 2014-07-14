
using Catrobat.IDE.Core.ViewModels;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Catrobat.IDE.WindowsPhone.Views
{
    public abstract class ViewPageBase : Page
    {
        protected abstract ViewModelBase GetViewModel();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            var viewModel = GetViewModel();
            viewModel.GoBackCommand.Execute(null);
            e.Handled = true;
        }
    }
}
