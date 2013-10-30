using System.ComponentModel;
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

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }

        private void TextBoxBroadcastMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.BroadcastMessage = TextBoxBroadcastMessage.Text;
        }
    }
}