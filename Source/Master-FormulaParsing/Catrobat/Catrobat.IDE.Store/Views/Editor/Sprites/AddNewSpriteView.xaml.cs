using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Windows.UI.Xaml.Controls;


namespace Catrobat.IDE.Store.Views.Editor.Sprites
{
    public sealed partial class AddNewSpriteView : UserControl
    {
        private readonly AddNewSpriteViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).AddNewSpriteViewModel;

        public AddNewSpriteView()
        {
            this.InitializeComponent();
        }

        private void TextBoxSpriteName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SpriteName = TextBoxSpriteName.Text;
        }
    }
}
