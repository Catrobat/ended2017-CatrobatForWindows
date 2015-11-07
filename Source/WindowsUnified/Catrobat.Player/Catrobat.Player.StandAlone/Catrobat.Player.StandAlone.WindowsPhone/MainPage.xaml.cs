using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Phone.UI.Input;
using Windows.Graphics.Display;
using Catrobat.Player.StandAlone.Parser;
using Catrobat.Player.StandAlone.Objects;

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

            // Grid for Player's content acquires hereby the whole height 
            // & is not compressed when the CommandBar fires up
            this.Loaded += (s, e) =>
            {
                mainRow.MaxHeight = mainRow.ActualHeight;
                mainRow.Height = new GridLength(mainRow.ActualHeight, GridUnitType.Pixel);
            };

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
        private readonly Catrobat_Player.Catrobat_PlayerAdapter playerObject =
            new Catrobat_Player.Catrobat_PlayerAdapter();
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Register hardware back button event
            HardwareButtons.BackPressed += OnHardwareBackButtonPressed;
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            XMLParser parser = new XMLParser();
            Project project = parser.FakeParsing();
            project.PersistProjectStructure();

            playerObject.InitPlayer(PlayerPage, "testalphavalue");
        }

        private void OnRestartButtonClicked(object sender, RoutedEventArgs e)
        {
            playerObject.RestartButtonClicked();
        }

        private void OnResumeButtonClicked(object sender, RoutedEventArgs e)
        {
            playerObject.ResumeButtonClicked();
        }
        private void OnThumbnailButtonClicked(object sender, RoutedEventArgs e)
        {
            playerObject.ThumbnailButtonClicked();
        }
        private void OnAxesButtonClicked(object sender, RoutedEventArgs e)
        {
            if (GridAxes.Visibility == Visibility.Visible)
            {
                playerObject.AxesButtonClicked(false, "axes on");
            }
            else
            {
                playerObject.AxesButtonClicked(true, "axes off");
            }
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
