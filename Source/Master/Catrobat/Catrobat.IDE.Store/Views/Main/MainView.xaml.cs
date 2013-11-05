using System;
using System.Collections.Generic;
using Catrobat.IDE.Core.Services;
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
        private MainViewModel _viewModel = ServiceLocator.GetInstance<MainViewModel>();

        public MainView()
        {
            this.InitializeComponent();

            var viewModelsOnPage = new List<Type>
            {
                typeof (MainViewModel)
            };

            this._navigationHelper = new NavigationHelper(this, viewModelsOnPage);
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
    }
}
