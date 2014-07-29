using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.CatrobatObjects;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class OnlineProjectView : ViewPageBase
    {
        private readonly OnlineProjectViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).OnlineProjectViewModel;     

        public OnlineProjectView()
        {
            InitializeComponent();
        }

        private void ViewPageBase_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _viewModel.OnLoadCommand.Execute((OnlineProjectHeader)DataContext);
        }

    }
}
