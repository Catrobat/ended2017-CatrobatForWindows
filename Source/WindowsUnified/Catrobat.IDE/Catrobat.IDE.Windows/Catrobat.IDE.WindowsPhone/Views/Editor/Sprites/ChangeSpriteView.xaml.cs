using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sprites
{
    public partial class ChangeSpriteView : Page
    {
        private readonly ChangeSpriteViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).ChangeSpriteViewModel;

        public ChangeSpriteView()
        {
            InitializeComponent();

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                TextBoxSpriteName.Focus(FocusState.Keyboard);
                TextBoxSpriteName.SelectAll();
            });
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Handled = true;
        }

        //protected override void OnBackKeyPress(CancelEventArgs e)
        //{
        //    _viewModel.GoBackCommand.Execute(null);
        //    e.Cancel = true;
        //    base.OnBackKeyPress(e);
        //}

        private void TextBoxSpriteName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SpriteName = TextBoxSpriteName.Text;
        }
    }
}