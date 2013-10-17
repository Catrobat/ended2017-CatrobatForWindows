using System;
using System.Windows;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Phone.ViewModel;
using Catrobat.IDE.Phone.Views.Main;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Controls.SplashScreen
{
    public partial class SplashScreen : PhoneApplicationPage
    {
        public SplashScreen()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // init app
            ViewModelLocator.LoadContext();

            ServiceLocator.NavigationService.NavigateTo(typeof(MainView));
        }
    }
}