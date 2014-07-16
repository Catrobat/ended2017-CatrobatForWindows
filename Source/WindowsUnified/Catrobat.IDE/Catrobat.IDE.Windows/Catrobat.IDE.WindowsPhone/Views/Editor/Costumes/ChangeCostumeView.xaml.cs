using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Costumes
{
    public partial class ChangeCostumeView
    {
        private readonly ChangeCostumeViewModel _viewModel = 
            (ServiceLocator.ViewModelLocator).ChangeCostumeViewModel;
        

        public ChangeCostumeView()
        {
            InitializeComponent();
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CostumeName = TextBoxCostumeName.Text;
        }
    }
}