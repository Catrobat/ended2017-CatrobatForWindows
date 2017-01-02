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
using Catrobat_Player.NativeComponent;

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
            // Grid for PLayer's content acquires hereby the whole height 
            // & is not compressed when the CommandBar fires up
            this.Loaded += (s, e) =>
            {
                mainRow.MaxHeight = mainRow.ActualHeight;
                mainRow.Height = new GridLength(mainRow.ActualHeight, GridUnitType.Pixel);
            };

            this.InitializeComponent();
        }
        
        
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (_viewModel.IsLaunchFromTile)
                while (ServiceLocator.NavigationService.CanGoBack)
                    ServiceLocator.NavigationService.RemoveBackEntry();

            var program = await ServiceLocator.ProgramValidationService.GetProgram(Path.Combine(StorageConstants.ProgramsPath, 
                ServiceLocator.ViewModelLocator.ProgramDetailViewModel.CurrentProgramHeader.ProjectName));

            NativeWrapper.SetProject(program);

            _playerObject.InitPlayer(PlayerPage, _viewModel.ProgramName);
            _viewModel.AxesVisible = false;
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

            _playerObject.Dispose();
            base.OnNavigatingFrom(e);
        }
    }
}
