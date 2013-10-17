using System.Windows.Navigation;
using Catrobat.IDE.Phone.ViewModel.Main;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Main
{
    public partial class ProjectImportView : PhoneApplicationPage
    {
        private readonly ProjectImportViewModel _viewModel = ServiceLocator.Current.GetInstance<ProjectImportViewModel>();

        public ProjectImportView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //string fileToken;
            //NavigationContext.QueryString.TryGetValue("fileToken", out fileToken);

            //_viewModel.OnLoadCommand.Execute(fileToken);
        }
    }
}