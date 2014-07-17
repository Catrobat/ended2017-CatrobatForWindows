using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sprites
{
    public partial class AddNewSpriteView
    {
        private readonly AddNewSpriteViewModel _viewModel = 
            (ServiceLocator.ViewModelLocator).AddNewSpriteViewModel;

        public AddNewSpriteView()
        {
            InitializeComponent();

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                TextBoxSpriteName.Focus(FocusState.Keyboard);
                TextBoxSpriteName.SelectAll();
            });
        }

        private void TextBoxSpriteName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SpriteName = TextBoxSpriteName.Text;
        }
    }
}