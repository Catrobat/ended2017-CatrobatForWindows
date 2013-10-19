using System;
using System.Windows;
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
        //TODO: maybe delete viewmodel
        private readonly PlayerLauncherViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).PlayerLauncherViewModel;
        public PlayerLauncherView()
        {
            InitializeComponent();
        }

        private readonly Direct3DBackground m_d3dBackground = new Direct3DBackground();

        private void DrawingSurfaceBackground_Loaded(object sender, RoutedEventArgs e)
        {
            // Set window bounds in dips
            m_d3dBackground.WindowBounds = new Size(
                (float)Application.Current.Host.Content.ActualWidth,
                (float)Application.Current.Host.Content.ActualHeight
                );

            // Set native resolution in pixels
            m_d3dBackground.NativeResolution = new Size(
                (float)Math.Floor(Application.Current.Host.Content.ActualWidth * Application.Current.Host.Content.ScaleFactor / 100.0f + 0.5f),
                (float)Math.Floor(Application.Current.Host.Content.ActualHeight * Application.Current.Host.Content.ScaleFactor / 100.0f + 0.5f)
                );

            // Set render resolution to the full native resolution
            m_d3dBackground.RenderResolution = m_d3dBackground.NativeResolution;

            // Set ProjectName to load
            var projectName = "";
            if (NavigationContext.QueryString.TryGetValue("ProjectName", out projectName))
            {
                m_d3dBackground.ProjectName = projectName;
            }

            // Hook-up native component to DrawingSurfaceBackgroundGrid
            DrawingSurfaceBackground.SetBackgroundContentProvider(m_d3dBackground.CreateContentProvider());
            DrawingSurfaceBackground.SetBackgroundManipulationHandler(m_d3dBackground);
        }
    }
}