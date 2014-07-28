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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.WindowsPhone.Controls.Projects
{
    public sealed partial class LocalProjectControl : UserControl
    {
        #region Dependancy properties

        public LocalProjectHeader Project
        {
            get { return (LocalProjectHeader)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }

        public static readonly DependencyProperty ProjectProperty =
            DependencyProperty.Register("Project",
            typeof(LocalProjectHeader),
            typeof(LocalProjectControl),
            new PropertyMetadata(null, ProjectChanged));

        private static void ProjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var localProjectControl = d as LocalProjectControl;
            if (localProjectControl != null) localProjectControl.DataContext = e.NewValue;
        }

        #endregion

        public LocalProjectControl()
        {
            this.InitializeComponent();
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            MenuFlyoutLocalProjectOptions.ShowAt(this);
        }

        private void NotValidButton_OnClick(object sender, RoutedEventArgs e)
        {
            var projectHeader = (LocalProjectHeader) DataContext;

            switch (projectHeader.ValidityState)
            {  
                case LocalProjectState.Damaged:
                    ServiceLocator.NotifictionService.ShowMessageBox(
                        AppResources.Main_SelectedProjectNotValidMessage,
                        String.Format(AppResources.Main_SelectedProjectNotValidHeader,
                        projectHeader.ProjectName),
                        delegate { /* no action */ }, MessageBoxOptions.Ok);
                    break;

                case LocalProjectState.AppUpdateRequired:
                    // TODO: show messagebox and offer link to the store
                    break;

                case LocalProjectState.VersionOutdated:
                    // TODO: show messagebox and maybe add functionality to repair the project
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            
           ;
        }
    }
}
