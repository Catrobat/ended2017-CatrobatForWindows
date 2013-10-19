using System.Windows;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Phone.ViewModel.Editor.Sprites;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor
{
    public partial class EditorLoadingView : PhoneApplicationPage
    {
        public EditorLoadingView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ServiceLocator.NavigationService.NavigateTo(typeof(SpritesViewModel));
            ServiceLocator.NavigationService.RemoveBackEntry();
        }
    }
}