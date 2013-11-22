using System.ComponentModel;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.Store.Views.Editor.Sprites
{
    public sealed partial class SpritesView : Page
    {
        private readonly SpritesViewModel _viewModel = ServiceLocator.GetInstance<SpritesViewModel>();

        public SpritesView()
        {
            this.InitializeComponent();
            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName ==
                PropertyHelper.GetPropertyName(() => _viewModel.SelectedSprite))
                AppBarBottomn.IsOpen = _viewModel.SelectedSprite != null;
        }

        private void FlyoutNew_OnOpen(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<AddNewSpriteViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }
    }
}
