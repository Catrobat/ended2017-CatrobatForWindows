using System.Windows.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Phone.ViewModel;
using Catrobat.IDE.Phone.ViewModel.Editor.Costumes;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Costumes
{
    public partial class CostumeNameChooserView : PhoneApplicationPage
    {
        private readonly CostumeNameChooserViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).CostumeNameChooserViewModel;

        public CostumeNameChooserView()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(() =>
                {
                    TextBoxCostumeName.Focus();
                    TextBoxCostumeName.SelectAll();
                });
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CostumeName = TextBoxCostumeName.Text;
        }
    }
}