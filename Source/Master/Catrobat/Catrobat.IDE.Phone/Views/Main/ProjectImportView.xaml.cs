using System.ComponentModel;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Main
{
    public partial class ProjectImportView : PhoneApplicationPage
    {
        private readonly ProjectImportViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).ProjectImportViewModel;

        public ProjectImportView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //string fileToken;
            //NavigationContext.QueryString.TryGetValue("fileToken", out fileToken);

            //_viewModel.OnLoadCommand.Execute(fileToken);
        }
    }
}