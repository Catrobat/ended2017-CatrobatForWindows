using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Costumes
{
    public partial class ChangeCostumeView : PhoneApplicationPage
    {
        private readonly ChangeCostumeViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).ChangeCostumeViewModel;

        public ChangeCostumeView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CostumeName = TextBoxCostumeName.Text;
        }
    }
}