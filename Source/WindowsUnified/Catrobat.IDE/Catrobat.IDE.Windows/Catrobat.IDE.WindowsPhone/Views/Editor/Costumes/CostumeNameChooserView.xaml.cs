using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Costumes
{
    public partial class CostumeNameChooserView
    {
        private readonly CostumeNameChooserViewModel _viewModel =
            ServiceLocator.ViewModelLocator.CostumeNameChooserViewModel;
        

        public CostumeNameChooserView()
        {
            InitializeComponent();

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                //TextBoxCostumeName.Focus(FocusState.Keyboard);
                if (TextBoxCostumeName != null) TextBoxCostumeName.SelectAll();
            });
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CostumeName = TextBoxCostumeName.Text;
        }
    }
}