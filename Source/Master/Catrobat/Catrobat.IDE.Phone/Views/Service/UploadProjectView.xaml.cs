using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Service;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Service
{
    public partial class UploadProjectView : PhoneApplicationPage
    {
        private readonly UploadProjectViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadProjectViewModel;

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