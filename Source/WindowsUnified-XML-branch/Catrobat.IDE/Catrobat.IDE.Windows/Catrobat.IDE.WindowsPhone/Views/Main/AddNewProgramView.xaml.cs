using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class AddNewProgramView
    {
        private readonly AddNewProgramViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.AddNewProgramViewModel;



        public AddNewProgramView()
        {
            InitializeComponent();

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                //TextBoxProjectName.Focus(FocusState.Keyboard);
                TextBoxProgramName.SelectAll();
            });
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}