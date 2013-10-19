using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.ViewModel.Service;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Service
{
    public partial class UploadProjectView : PhoneApplicationPage
    {
        private readonly UploadProjectViewModel _viewModel = ServiceLocator.Current.GetInstance<UploadProjectViewModel>();

        public UploadProjectView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.InitializeCommand.Execute(null);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // otherwise bound properties won't get set
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}