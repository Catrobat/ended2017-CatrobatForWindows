using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;


namespace Catrobat.IDE.Store.Views.Editor.Costumes
{
    public sealed partial class CostumeNameChooserView : Page
    {
        private readonly CostumeNameChooserViewModel _viewModel = 
            (ServiceLocator.ViewModelLocator).CostumeNameChooserViewModel;

        public CostumeNameChooserView()
        {
            this.InitializeComponent();
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CostumeName = TextBoxCostumeName.Text;
        }
    }
}
