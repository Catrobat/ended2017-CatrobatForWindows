using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Main;
using Catrobat.IDE.Store.Common;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Hub Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=286574

namespace Catrobat.IDE.Store.Views.Main
{
    public sealed partial class MainView : Page
    {
        private readonly NavigationHelper _navigationHelper;
        private readonly MainViewModel _viewModel = ServiceLocator.GetInstance<MainViewModel>();

        public MainView()
        {
            this.InitializeComponent();

            var viewModelsOnPage = new List<Type>
            {
                typeof (MainViewModel)
            };

            this._navigationHelper = new NavigationHelper(this, viewModelsOnPage);

            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName ==
                PropertyHelper.GetPropertyName(() => _viewModel.SelectedLocalProject))
                AppBarBottomn.IsOpen = _viewModel.SelectedLocalProject != null;
        }

        private void FlyoutNewProject_OnOpened(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<AddNewProjectViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
            viewModel.ResetViewModel();
        }

        private void ChangeProjectFlyout_OnOpened(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<ProjectSettingsViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }

        private void TileGeneratorFlyout_OnOpened(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<TileGeneratorViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }

        #region NavigationHelper registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void AppBarBottomn_OnOpened(object sender, object e)
        {
            if (_viewModel.SelectedLocalProject == null)
                AppBarBottomn.IsOpen = false;
        }
    }
}