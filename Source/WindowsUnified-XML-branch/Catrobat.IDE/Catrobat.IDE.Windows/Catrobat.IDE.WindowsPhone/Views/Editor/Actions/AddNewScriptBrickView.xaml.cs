using Windows.UI.Xaml.Input;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Actions;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.WindowsPhone.Controls.ListsViewControls;
using Catrobat.IDE.WindowsPhone.Controls.ListsViewControls.CatrobatListView.CatrobatListViewMisc;
using Windows.UI.Xaml;
using Catrobat.IDE.WindowsPhone.Controls.ListsViewControls.CatrobatListView;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Actions
{
    public partial class AddNewScriptBrickView
    {
        private readonly AddNewScriptBrickViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.AddNewScriptBrickViewModel;

        public AddNewScriptBrickView()
        {
            InitializeComponent();

            var bounds = Window.Current.Bounds;

            double height = bounds.Height * 0.88;
            double width = bounds.Width * 0.95;

            CatrobatListView view = this.FindName("Top") as CatrobatListView;

            if (view != null)
            {
                view.ItemWidthLandscape = (int)height;
                view.ItemWidthPortrait = (int)width;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.OnLoadBrickViewCommand.Execute(null);
        }

        private void CatrobatListView_OnItemTapped(object sender, CatrobatListViewItemEventArgs e)
        {
            _viewModel.AddNewScriptBrickCommand.Execute(e.GetTappedItem().Content as ModelBase);
        }
    }
}