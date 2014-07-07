using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;

namespace Catrobat.IDE.Store.Views.Editor.Costumes
{
    public sealed partial class NewCostumeSourceSelectionView : UserControl
    {
        private readonly NewCostumeSourceSelectionViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).NewCostumeSourceSelectionViewModel;

        public NewCostumeSourceSelectionView()
        {
            this.InitializeComponent();
        }

        private async void ButtonOpenGallery_OnClick(object sender, RoutedEventArgs e)
        {
            await _viewModel.OpenGalleryAction();
        }

        private async void ButtonCamera_OnClick(object sender, RoutedEventArgs e)
        {
            await _viewModel.OpenCameraAction();
        }
    }
}
