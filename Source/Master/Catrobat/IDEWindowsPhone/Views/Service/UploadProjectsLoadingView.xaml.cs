using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.Misc;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Views.Service
{
    public partial class UploadProjectsLoadingView : PhoneApplicationPage
    {
        public UploadProjectsLoadingView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ProgressBarProgress.IsIndeterminate = true;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ProgressBarProgress.IsIndeterminate = false;
            base.OnNavigatedFrom(e);
        }
    }
}