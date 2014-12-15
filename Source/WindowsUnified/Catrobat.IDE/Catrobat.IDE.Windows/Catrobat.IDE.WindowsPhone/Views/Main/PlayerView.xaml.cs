using System;
//using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Main;
//using Windows.Foundation;
//using Windows.Foundation.Collections;
//using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
//using Windows.UI.Xaml.Controls.Primitives;
//using Windows.UI.Xaml.Data;
//using Windows.UI.Xaml.Input;
//using Windows.UI.Xaml.Media;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.Activation;

using Catrobat_Player;
//using Catrobat.IDE.Core.CatrobatObjects;
//using Catrobat.IDE.Core.Resources.Localization;
//using Catrobat.IDE.Core.ViewModels;

//using Windows.System;
//using Windows.UI.Xaml.Controls;

//using Windows.UI.Xaml.Navigation;
//using Catrobat.IDE.WindowsShared.Misc;

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
            InitializeComponent();

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //SetSourceOfThumbnail(); --> TODO it is necessary to change the SplashScreen image instead of setting an image on this XAML page

            if (_viewModel.IsLaunchFromTile)
                while (ServiceLocator.NavigationService.CanGoBack)
                    ServiceLocator.NavigationService.RemoveBackEntry();

            _playerObject.InitPlayer(swapChainPanel, PlayerAppBar, _viewModel.ProjectName);
            _viewModel.PlayerObject = _playerObject;

            base.OnNavigatedTo(e);
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
