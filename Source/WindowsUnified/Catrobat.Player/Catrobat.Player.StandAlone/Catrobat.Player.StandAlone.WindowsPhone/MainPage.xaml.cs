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
using Windows.Phone.UI.Input;




// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Catrobat.Player.StandAlone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        //private readonly Catrobat_Player::Catrobat_PlayerMain _testMain = new Catrobat_PlayerMain;

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>

        //var playerObject = null;
        private readonly Catrobat_Player.Catrobat_PlayerAdapter playerObject = new Catrobat_Player.Catrobat_PlayerAdapter();
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // register hardware back button event
            HardwareButtons.BackPressed += OnHardwareBackButtonPressed;

            playerObject.InitPlayer(swapChainPanel, PlayerAppBar, "testTapp2");
        }

        private void OnRestartButtonClicked(object sender, RoutedEventArgs e)
        {
            playerObject.RestartButtonClicked();            
        }

        private void OnPlayButtonClicked(object sender, RoutedEventArgs e)
        {
            playerObject.PlayButtonClicked();
        }
        private void OnThumbnailButtonClicked(object sender, RoutedEventArgs e)
        {
            playerObject.ThumbnailButtonClicked();
        }
        private void OnEnableAxisButtonClicked(object sender, RoutedEventArgs e)
        {
            playerObject.EnableAxisButtonClicked();
        }
        private void OnScreenshotButtonClicked(object sender, RoutedEventArgs e)
        {
            playerObject.ScreenshotButtonClicked();
        }

        private void OnHardwareBackButtonPressed(object sender, BackPressedEventArgs args)
        {
            args.Handled = true;
            if (playerObject.HardwareBackButtonPressed() == true)
            {
                Application.Current.Exit();
            }
        }
    }
}
