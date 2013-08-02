using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
    public partial class ProjectNotValidView : PhoneApplicationPage
    {
        private readonly ProjectNotValidViewModel _viewModel = ServiceLocator.Current.GetInstance<ProjectNotValidViewModel>();

        public ProjectNotValidView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.FinishedCommand.Execute(null);
        }
    }
}