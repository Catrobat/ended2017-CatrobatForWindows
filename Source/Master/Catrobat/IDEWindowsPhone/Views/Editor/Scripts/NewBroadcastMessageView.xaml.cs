using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.ViewModel.Scripts;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Scripts
{
    public partial class NewBroadcastMessageView : PhoneApplicationPage
    {
        private readonly NewBroadcastMessageViewModel _viewModel = ServiceLocator.Current.GetInstance<NewBroadcastMessageViewModel>();

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