using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Main;
using Catrobat.IDE.Store.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Catrobat.IDE.Store.Views.Main
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class PlayerLauncherView : Page
    {

        private readonly NavigationHelper _navigationHelper;
        private PlayerLauncherViewModel _viewModel = ServiceLocator.GetInstance<PlayerLauncherViewModel>();

        public PlayerLauncherView()
        {
            this.InitializeComponent();

            var viewModelsOnPage = new List<Type>
            {
                typeof (PlayerLauncherViewModel)
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
