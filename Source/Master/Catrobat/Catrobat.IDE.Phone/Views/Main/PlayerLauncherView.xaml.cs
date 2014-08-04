using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Microsoft.Phone.Controls;
using Size = Windows.Foundation.Size;
using PhoneDirect3DXamlAppComponent;
using Microsoft.Phone.Shell;
using Catrobat.IDE.Core.Resources.Localization;

namespace Catrobat.IDE.Phone.Views.Main
{
    public partial class PlayerLauncherView : PhoneApplicationPage
    {
        private readonly PlayerLauncherViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).PlayerLauncherViewModel;

        public PlayerLauncherView()
        {
            InitializeComponent();
            InitializeApplicationBar();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            ApplicationBar.IsVisible = ApplicationBar.IsVisible ? false : true;
            e.Cancel = true;
        }

        private void InitializeApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Default;
            ApplicationBar.Opacity = 1.0;
            ApplicationBar.IsVisible = false;
            ApplicationBar.IsMenuEnabled = true;

            ApplicationBarIconButton backButton = new ApplicationBarIconButton();
            backButton.IconUri = new Uri("/Content/Images/ApplicationBar/dark/appbar.back.rest.png", UriKind.Relative);
            backButton.Text = AppResources.Player_Back;
            backButton.Click += backButton_Click;
            ApplicationBar.Buttons.Add(backButton);

            ApplicationBarIconButton restartButton = new ApplicationBarIconButton();
            restartButton.IconUri = new Uri("/Content/Images/ApplicationBar/dark/appbar.refresh.rest.png", UriKind.Relative);
            restartButton.Text = AppResources.Player_Restart;
            restartButton.Click += restartButton_Click;
            ApplicationBar.Buttons.Add(restartButton);

            ApplicationBarIconButton resumeButton = new ApplicationBarIconButton();
            resumeButton.IconUri = new Uri("/Content/Images/ApplicationBar/dark/appbar.transport.play.rest.png", UriKind.Relative);
            resumeButton.Text = AppResources.Player_Resume;
            resumeButton.Click += resumeButton_Click;
            ApplicationBar.Buttons.Add(resumeButton);

            ApplicationBarIconButton screenshotButton = new ApplicationBarIconButton();
            screenshotButton.IconUri = new Uri("/Content/Images/ApplicationBar/dark/appbar.feature.camera.rest.png", UriKind.Relative);
            screenshotButton.Text = AppResources.Player_TakeScreenshot;
            screenshotButton.Click += screenshotButton_Click;
            ApplicationBar.Buttons.Add(screenshotButton);

            ApplicationBarMenuItem axisMenueItem = new ApplicationBarMenuItem();
            axisMenueItem.Text = AppResources.Player_AxisOn;
            axisMenueItem.Click += axisMenueItem_Click;
            ApplicationBar.MenuItems.Add(axisMenueItem);
        }

        void axisMenueItem_Click(object sender, EventArgs e)
        {
            var menueItem = (sender as ApplicationBarMenuItem);
            menueItem.Text = menueItem.Text == AppResources.Player_AxisOn ? AppResources.Player_AxisOff : AppResources.Player_AxisOn;
            //TODO: Implement
        }

        void resumeButton_Click(object sender, EventArgs e)
        {
            //TODO: Implement
        }

        void screenshotButton_Click(object sender, EventArgs e)
        {
            //TODO: Implement
        }

        void restartButton_Click(object sender, EventArgs e)
        {
            //TODO: Implement
        }

        void backButton_Click(object sender, EventArgs e)
        {
            ServiceLocator.NavigationService.NavigateBack();
        }

        private readonly Direct3DBackground _d3DBackground = new Direct3DBackground();

        private void DrawingSurfaceBackground_Loaded(object sender, RoutedEventArgs e)
        {
            // Set window bounds in dips
            _d3DBackground.WindowBounds = new Size(
                (float)Application.Current.Host.Content.ActualWidth,
                (float)Application.Current.Host.Content.ActualHeight
                );

            // Set native resolution in pixels
            _d3DBackground.NativeResolution = new Size(
                (float)Math.Floor(Application.Current.Host.Content.ActualWidth * Application.Current.Host.Content.ScaleFactor / 100.0f + 0.5f),
                (float)Math.Floor(Application.Current.Host.Content.ActualHeight * Application.Current.Host.Content.ScaleFactor / 100.0f + 0.5f)
                );

            // Set render resolution to the full native resolution
            _d3DBackground.RenderResolution = _d3DBackground.NativeResolution;
            _d3DBackground.ProjectName = _viewModel.PlayProjectName;

            // Hook-up native component to DrawingSurfaceBackgroundGrid
            DrawingSurfaceBackground.SetBackgroundContentProvider(_d3DBackground.CreateContentProvider()); // TODO: Chrashes if launched from an pinned project tile
            DrawingSurfaceBackground.SetBackgroundManipulationHandler(_d3DBackground);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (_viewModel.IsLauncheFromTile && ServiceLocator.NavigationService.CanGoBack)
                ServiceLocator.NavigationService.RemoveBackEntry();

            base.OnNavigatedTo(e);
        }
    }
}