using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Main;
using Catrobat.IDE.Store.Services;
using GalaSoft.MvvmLight;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;

namespace Catrobat.IDE.Store.Controls
{
    public sealed partial class SplashScreen : Page
    {
        public SplashScreen()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!ViewModelBase.IsInDesignModeStatic)
            {
                Core.App.SetNativeApp(new AppStore());
                await Core.App.Initialize();

                var image = new BitmapImage(new Uri("ms-appx:///Content/Images/Screenshot/NoScreenshot.png", UriKind.Absolute))
                {
                    CreateOptions = BitmapCreateOptions.None
                };
                image.ImageOpened += (o, args) =>
                {

                };
                ManualImageCache.NoScreenshotImage = image;
                ServiceLocator.NavigationService = new NavigationServiceStore(Frame);
            }

            ServiceLocator.NavigationService.NavigateTo(typeof(MainViewModel));
        }
    }
}
