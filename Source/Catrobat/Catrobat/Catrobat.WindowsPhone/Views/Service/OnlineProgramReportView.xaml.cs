using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class OnlineProgramReportView : ViewPageBase
    {
        private readonly OnlineProgramReportViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).OnlineProgramReportViewModel;     

        public OnlineProgramReportView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}
