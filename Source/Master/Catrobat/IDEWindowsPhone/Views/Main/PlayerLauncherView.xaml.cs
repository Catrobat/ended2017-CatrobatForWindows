using System;
using System.Windows;
using System.Windows.Navigation;
using Catrobat.Core;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;
using PhoneDirect3DXamlAppComponent;
using Size = Windows.Foundation.Size;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
    public partial class PlayerLauncherView : PhoneApplicationPage
    {
        public static bool IsNavigateBack = true;

        public PlayerLauncherView()
        {
            InitializeComponent();
            //Loaded += OnLoaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationContext.QueryString.ContainsKey("ProjectName"))
            {
                var projectName = NavigationContext.QueryString["ProjectName"];

                try
                {
                    CatrobatContextBase.GetContext().SetCurrentProject(projectName);
                }
                catch (Exception)
                {
                    var projectNotValidViewModel = ServiceLocator.Current.GetInstance<ProjectNotValidViewModel>();
                    projectNotValidViewModel.Error = PlayerLaunchingError.ProjectDosNotExist;
                    Navigation.NavigateTo(typeof(ProjectNotValidView));
                }
            }
            else
            {
                if (IsNavigateBack)
                    Navigation.NavigateBack();
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            IsNavigateBack = true;
            base.OnNavigatedFrom(e);
        }


        private readonly Direct3DBackground m_d3dBackground = new Direct3DBackground();
        //private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        //{
        //    //var projectName = string.Empty;
        //    //if (NavigationContext.QueryString.TryGetValue("ProjectName", out projectName))
        //    //{
        //    //}
        //}

        private void ProjectNotFoundMessageResult(MessageBoxResult result)
        {
            Navigation.NavigateBack();
        }

        private void DrawingSurfaceBackground_Loaded(object sender, RoutedEventArgs e)
        {
            // Set window bounds in dips
            m_d3dBackground.WindowBounds = new Size(
                (float) Application.Current.Host.Content.ActualWidth,
                (float) Application.Current.Host.Content.ActualHeight
                );

            // Set native resolution in pixels
            m_d3dBackground.NativeResolution = new Size(
                (float) Math.Floor(Application.Current.Host.Content.ActualWidth*Application.Current.Host.Content.ScaleFactor/100.0f + 0.5f),
                (float) Math.Floor(Application.Current.Host.Content.ActualHeight*Application.Current.Host.Content.ScaleFactor/100.0f + 0.5f)
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