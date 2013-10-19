using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Scripts;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Scripts
{
    public partial class NewBroadcastMessageView : PhoneApplicationPage
    {
        private readonly NewBroadcastMessageViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).NewBroadcastMessageViewModel;

        public NewBroadcastMessageView()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(() =>
                {
                    TextBoxBroadcastMessage.Focus();
                    TextBoxBroadcastMessage.SelectAll();
                });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }

        private void TextBoxBroadcastMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.BroadcastMessage = TextBoxBroadcastMessage.Text;
        }
    }
}