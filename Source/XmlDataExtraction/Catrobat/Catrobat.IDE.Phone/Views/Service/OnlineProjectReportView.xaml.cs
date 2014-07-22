using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Microsoft.Phone.Controls;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Phone.Views.Service
{
    public partial class OnlineProjectReportView : PhoneApplicationPage
    {
        private readonly OnlineProjectReportViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).OnlineProjectReportViewModel;

        public OnlineProjectReportView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // otherwise bound properties won't get set
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}