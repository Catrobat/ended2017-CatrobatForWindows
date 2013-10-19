using System.Windows.Controls;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Costumes
{
    public partial class CostumeNameChooserView : PhoneApplicationPage
    {
        private readonly AddNewCostumeViewModel _viewModel = ServiceLocator.Current.GetInstance<AddNewCostumeViewModel>();

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