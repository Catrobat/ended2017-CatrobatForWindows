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

namespace Catrobat.IDE.WindowsPhone.Controls.Projects
{
    public sealed partial class LocalProjectControl : UserControl
    {
        #region Dependancy properties

        public ProjectDummyHeader Project
        {
            get { return (ProjectDummyHeader)GetValue(ProjectProperty); }
            set { SetValue(ProjectProperty, value); }
        }

        public static readonly DependencyProperty ProjectProperty = DependencyProperty.Register("Project", typeof(ProjectDummyHeader), typeof(LocalProjectControl), new PropertyMetadata(null, ProjectChanged));

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
    }
}
