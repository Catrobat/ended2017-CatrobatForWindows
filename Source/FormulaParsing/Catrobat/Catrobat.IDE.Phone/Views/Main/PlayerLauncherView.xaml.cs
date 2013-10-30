using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Main;
using Microsoft.Phone.Controls;
using Size = Windows.Foundation.Size;
using PhoneDirect3DXamlAppComponent;

namespace Catrobat.IDE.Phone.Views.Main
{
    public partial class PlayerLauncherView : PhoneApplicationPage
    {
        private readonly PlayerLauncherViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).PlayerLauncherViewModel;

        public PlayerLauncherView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
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

            // Set ProjectName to load
            var projectName = "";
            if (NavigationContext.QueryString.TryGetValue("ProjectName", out projectName))
            {
                _d3DBackground.ProjectName = projectName;
            }

            // Hook-up native component to DrawingSurfaceBackgroundGrid
            DrawingSurfaceBackground.SetBackgroundContentProvider(_d3DBackground.CreateContentProvider());
            DrawingSurfaceBackground.SetBackgroundManipulationHandler(_d3DBackground);
        }
    }
}