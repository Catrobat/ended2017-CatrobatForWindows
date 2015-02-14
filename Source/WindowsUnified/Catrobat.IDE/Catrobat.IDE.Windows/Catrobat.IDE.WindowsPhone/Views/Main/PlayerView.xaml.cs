using System;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.ViewManagement;
using Windows.Graphics.Display;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.Activation;

using Catrobat_Player;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.WindowsShared.Services;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class PlayerView
    {
        private readonly PlayerViewModel _viewModel =
            ServiceLocator.ViewModelLocator.PlayerViewModel;
        private Catrobat_Player.Catrobat_PlayerAdapter _playerObject = 
            new Catrobat_Player.Catrobat_PlayerAdapter();

        public PlayerView()
        {
            // Grid for page's content acquires hereby the whole height 
            // & is not compressed when the CommandBar fires up
            this.Loaded += (s, e) =>
            {
                mainRow.MaxHeight = mainRow.ActualHeight;
                mainRow.Height = new GridLength(mainRow.ActualHeight, GridUnitType.Pixel);
            };

            this.InitializeComponent();
        }
        
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //SetSourceOfThumbnail(); --> TODO it is necessary to change the SplashScreen image instead of setting an image on this XAML page

            if (_viewModel.IsLaunchFromTile)
                while (ServiceLocator.NavigationService.CanGoBack)
                    ServiceLocator.NavigationService.RemoveBackEntry();

            _playerObject.InitPlayer(PlayerPage, _viewModel.ProjectName);
            PlayerLauncherServiceWindowsShared.SetPlayerObject(_playerObject);

            Window.Current.Activate();

            // Portrait only for the Player for now
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            // Portrait only for the Player for now --> set back the default value
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait
                | DisplayOrientations.Landscape | DisplayOrientations.LandscapeFlipped;

            base.OnNavigatingFrom(e);
        }

        /*private async void SetSourceOfThumbnail()
        {
            var projectFolder = "ms-appdata:///Local/" + StorageConstants.ProgramsPath + "/" + _viewModel.ProjectName + "/";

            StorageFile file = null;
            try
            {
                var manualScreenshotPath = projectFolder + StorageConstants.ProgramManualScreenshotPath;
                file = await StorageFile.GetFileFromApplicationUriAsync(
                        new Uri(manualScreenshotPath, UriKind.Absolute));
            } 
            catch (Exception e)
            {
                file = null;                
            }

            if (file == null)
            {
                try
                {
                    var automaticProjectScreenshotPath = projectFolder + StorageConstants.ProgramAutomaticScreenshotPath;
                    file = await StorageFile.GetFileFromApplicationUriAsync(
                            new Uri(automaticProjectScreenshotPath, UriKind.Absolute));
                }
                catch (Exception e)
                {
                    file = null;
                }
            }

            if (file != null)
            {
                var randomAccessStream = await file.OpenReadAsync();
                var playerSplashImage = new BitmapImage()
                {
                    CreateOptions = BitmapCreateOptions.None
                };

                //playerSplashImage.DecodePixelHeight = (int)Window.Current.Bounds.Height;
                //playerSplashImage.DecodePixelWidth = (int)Window.Current.Bounds.Width;
                
                await playerSplashImage.SetSourceAsync(randomAccessStream);
                ImagePlayerLoadSplashScreen.Source = playerSplashImage;
            }
        } */

    }
}
