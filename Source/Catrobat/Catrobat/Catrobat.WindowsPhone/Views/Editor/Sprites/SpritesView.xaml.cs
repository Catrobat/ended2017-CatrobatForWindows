using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.WindowsPhone.Controls;
using Catrobat.IDE.WindowsPhone.Controls.ListsViewControls.CatrobatListView;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sprites
{
    public partial class SpritesView
    {
        private readonly SpritesViewModel _viewModel =
            ServiceLocator.ViewModelLocator.SpritesViewModel;

        public SpritesView()
        {
            //NavigationCacheMode = NavigationCacheMode.Enabled;
            InitializeComponent();

            var bounds = Window.Current.Bounds;

            double height = bounds.Height * 0.88;
            double width = bounds.Width * 0.95;

            Rectangle backgroundLine = this.FindName("BackgroundLine") as Rectangle;
            backgroundLine.Width = (int) width - 80;

            Rectangle spritesLine = this.FindName("SpritesLine") as Rectangle;
            spritesLine.Width = (int)width - 80;

            CatrobatListView view = this.FindName("ListViewSprites") as CatrobatListView;
            view.ItemWidthLandscape = (int)height;
            view.ItemWidthPortrait = (int)width;


            CatrobatListView view2 = this.FindName("ListViewBackground") as CatrobatListView;
            view2.ItemWidthLandscape = (int)height;
            view2.ItemWidthPortrait = (int)width;

        }

        private void MultiModeEditorCommandBar_OnModeChanged(MultiModeEditorCommandBarMode mode)
        {
            var listView = ListViewSprites;

            if (listView == null) return;

            switch (mode)
            {
                case MultiModeEditorCommandBarMode.Normal:
                    listView.ReorderEnabled = false;
                    listView.SelectionEnabled = false;
                    _viewModel.CurrentProgram.Save();
                    break;
                case MultiModeEditorCommandBarMode.Reorder:
                    listView.SelectionEnabled = false;
                    listView.ReorderEnabled = true;
                    break;
                case MultiModeEditorCommandBarMode.Select:
                    listView.SelectionEnabled = true;
                    listView.ReorderEnabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }

        private void SpriteItem_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var isClickEnabled = !ListViewSprites.SelectionEnabled;

            if (isClickEnabled)
                if (_viewModel.EditSpriteCommand.CanExecute(((FrameworkElement)e.OriginalSource).DataContext))
                    _viewModel.EditSpriteCommand.Execute(((FrameworkElement)e.OriginalSource).DataContext);
        }
    }
}
