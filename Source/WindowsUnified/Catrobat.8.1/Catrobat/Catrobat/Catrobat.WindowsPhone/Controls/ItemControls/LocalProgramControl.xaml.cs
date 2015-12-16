using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.WindowsPhone.Controls
{
    public sealed partial class LocalProgramControl : UserControl
    {
        #region Dependancy properties

        public LocalProgramHeader Project
        {
            get { return (LocalProgramHeader)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }

        public static readonly DependencyProperty ProjectProperty =
            DependencyProperty.Register("Project",
            typeof(LocalProgramHeader),
            typeof(LocalProgramControl),
            new PropertyMetadata(null, ProjectChanged));

        private static void ProjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var localProjectControl = d as LocalProgramControl;
            if (localProjectControl != null) localProjectControl.DataContext = e.NewValue;
        }

        #endregion

        public LocalProgramControl()
        {
            this.InitializeComponent();
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            MenuFlyoutLocalProjectOptions.ShowAt(this);
        }

        private void NotValidButton_OnClick(object sender, RoutedEventArgs e)
        {
            var projectHeader = (LocalProgramHeader) DataContext;

            
            //switch (projectHeader.ValidityState)
            //{
            //    case LocalProgramState.Damaged:
            //        ServiceLocator.NotifictionService.ShowMessageBox(
            //            AppResources.Main_SelectedProgramNotValidMessage,
            //            String.Format(AppResources.Main_SelectedProgramNotValidHeader,
            //            projectHeader.ProjectName),
            //            delegate { /* no action */ }, MessageBoxOptions.Ok);
            //        break;

            //    case LocalProgramState.AppUpdateRequired:
            //        // TODO: show messagebox and offer link to the store
            //        break;

            //    case LocalProgramState.VersionOutdated:
            //        // TODO: show messagebox and maybe add functionality to repair the project
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}
        }
    }
}
