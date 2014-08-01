using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Looks
{
    public partial class LookNameChooserView
    {
        private readonly LookNameChooserViewModel _viewModel =
            ServiceLocator.ViewModelLocator.LookNameChooserViewModel;
        

        public LookNameChooserView()
        {
            InitializeComponent();

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                //TextBoxLookName.Focus(FocusState.Keyboard);
                if (TextBoxLookName != null) TextBoxLookName.SelectAll();
            });
        }

        private void TextBoxLookName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.LookName = TextBoxLookName.Text;
        }
    }
}