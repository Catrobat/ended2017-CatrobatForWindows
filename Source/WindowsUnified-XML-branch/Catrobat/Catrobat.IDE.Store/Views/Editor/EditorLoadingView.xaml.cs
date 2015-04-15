using Catrobat.IDE.Core.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;

namespace Catrobat.IDE.Store.Views.Editor
{
    public sealed partial class EditorLoadingView : Page
    {
        public EditorLoadingView()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(SpritesViewModel));
            ServiceLocator.NavigationService.RemoveBackEntry();
        }
    }
}
