using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.Store.Views.Editor.Sprites
{
    public sealed partial class SpritesView : Page
    {
        public SpritesView()
        {
            this.InitializeComponent();
        }

        private void FlyoutNew_OnOpen(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<AddNewSpriteViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }
    }
}
