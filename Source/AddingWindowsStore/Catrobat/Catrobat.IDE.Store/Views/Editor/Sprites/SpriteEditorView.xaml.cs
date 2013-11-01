using System;
using System.Collections.ObjectModel;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.Store.Views.Editor.Sprites
{
    public sealed partial class SpriteEditorView : Page
    {
        readonly SpriteEditorViewModel _viewModel = ServiceLocator.GetInstance<SpriteEditorViewModel>();

        public SpriteEditorView()
        {
            this.InitializeComponent();

            //GridViewLooks.BindableSelectedItems = _viewModel.SelectedCostumes;
        }

        private void FlyoutChangeName_OnOpening(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<ChangeSpriteViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }
    }
}
