using System;
using System.Windows.Controls;
using Catrobat.Core.Objects.Costumes;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using System.Windows.Navigation;

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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }

        private void TextBoxCostumeName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.CostumeName = TextBoxCostumeName.Text;
        }
    }
}
