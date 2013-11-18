using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;

namespace Catrobat.IDE.Store.Views.Editor.Sounds
{
    public sealed partial class SoundNameChooserView : Page
    {
        private readonly CostumeNameChooserViewModel _viewModel = 
            (ServiceLocator.ViewModelLocator).CostumeNameChooserViewModel;

        public SoundNameChooserView()
        {
            this.InitializeComponent();
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CostumeName = TextBoxCostumeName.Text;
        }
    }
}
