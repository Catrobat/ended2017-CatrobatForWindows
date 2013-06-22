using System;
using System.Windows.Controls;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using Catrobat.IDEWindowsPhone.ViewModel.Scripts;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Navigation;

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