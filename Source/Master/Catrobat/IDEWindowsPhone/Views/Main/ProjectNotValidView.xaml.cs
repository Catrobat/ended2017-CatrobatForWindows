using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.Misc;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
    public partial class ProjectNotValidView : PhoneApplicationPage
    {
        public ProjectNotValidView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            Navigation.NavigateTo(typeof (MainView));
            //base.OnBackKeyPress(e);
        }
    }
}