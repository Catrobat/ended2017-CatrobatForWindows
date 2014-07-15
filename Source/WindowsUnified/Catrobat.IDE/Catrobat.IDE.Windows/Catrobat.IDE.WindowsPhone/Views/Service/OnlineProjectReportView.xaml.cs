using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class OnlineProjectReportView : ViewPageBase
    {
        private readonly OnlineProjectReportViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).OnlineProjectReportViewModel;

        protected override ViewModelBase GetViewModel() { return _viewModel; }

        public OnlineProjectReportView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
