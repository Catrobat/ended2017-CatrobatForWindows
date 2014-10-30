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
using Catrobat.Player;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

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

            //std::shared_ptr<DX::DeviceResources> m_deviceResources;
            //std::unique_ptr<Catrobat_PlayerMain> m_main; 

            //m_deviceResources = std::make_shared<DX::DeviceResources>();
            //m_deviceResources->SetSwapChainPanel(swapChainPanel);
            
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void OnRestartButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnPlayButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnThumbnailButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnEnableAxisButtonClicked(object sender, RoutedEventArgs e)
        {

        }

        private void OnScreenshotButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
