using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Catrobat.IDE.Store.Views.Editor.Sprites
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class SpritesView : Page
    {
        public SpritesView()
        {
            this.InitializeComponent();
        }

        private void FlyoutNew_OnClose(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<AddNewSpriteViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }
    }
}
